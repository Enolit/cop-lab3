using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class StudentForm : Form
    {
        private StudentContext _context;
        private Student _currentStudent; // Объект для редактирования
        private bool _isDirty;

        private string _originalFullName;
        private string _originalDescription;
        private int _originalCourse;
        private int _originalScholarship;

        public StudentForm(StudentContext context, Student student = null)
        {
            InitializeComponent();

            comboBoxCourse.AddList(new List<string> { "1", "2", "3", "4" });

            _context = context;
            _currentStudent = student;

            if (_currentStudent != null)
            {
                LoadStudentData(); // Загрузка данных студента для редактирования

                // Сохранение исходных значений для сравнения
                _originalFullName = _currentStudent.FullName;
                _originalDescription = _currentStudent.Description;
                _originalCourse = _currentStudent.Course;
                _originalScholarship = _currentStudent.Scholarship;
            }


            // Подписка на события изменения
            textBoxFullName.TextChanged += (s, e) => _isDirty = true;
            textBoxDescription.TextChanged += (s, e) => _isDirty = true;
            comboBoxCourse.SelectedElementChange += (s, e) => _isDirty = true;
            controlScholarship.ElementChanged += (s, e) => _isDirty = true;

        }

        private void LoadStudentData()
        {
            textBoxFullName.Text = _currentStudent.FullName;
            textBoxDescription.Text = _currentStudent.Description;
            comboBoxCourse.SelectedElement = _currentStudent.Course.ToString();
            controlScholarship.Value = _currentStudent.Scholarship;
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность всех обязательных полей
            if (string.IsNullOrWhiteSpace(textBoxFullName.Text))
            {
                MessageBox.Show("Поле ФИО не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
            {
                MessageBox.Show("Поле Описание не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxCourse.SelectedElement == null || !int.TryParse(comboBoxCourse.SelectedElement, out _))
            {
                MessageBox.Show("Поле Курс не может быть пустым или некорректным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var scholarship = controlScholarship.Value ?? 0;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Поле Стипендия не может быть пустым или некорректным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_currentStudent == null)
            {
                // Создание нового студента
                _currentStudent = new Student();
            }

            // Сохранение значений из полей формы в объекте
            _currentStudent.FullName = textBoxFullName.Text;
            _currentStudent.Description = textBoxDescription.Text; // Проверка на заполненность
            _currentStudent.Scholarship = controlScholarship.Value ?? 0;

            // Получаем значение курса из комбобокса
            if (int.TryParse(comboBoxCourse.SelectedElement, out int selectedCourse))
            {
                _currentStudent.Course = selectedCourse;
            }

            if (_currentStudent.Id == 0) // Если новый студент
            {
                _context.Students.Add(_currentStudent);
            }
            else
            {
                _context.Students.Update(_currentStudent);
            }

            _context.SaveChanges();

            // Выводим сообщение об успешном сохранении
            MessageBox.Show("Запись успешно сохранена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _isDirty = false; // Сбрасываем состояние dirty

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void StudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Проверка на несохраненные изменения
            if (_isDirty)
            {
                var result = MessageBox.Show("Есть несохраненные изменения. Вы уверены, что хотите закрыть?", "Подтверждение", MessageBoxButtons.YesNo);
                e.Cancel = (result == DialogResult.No);
            }
        }


        // Метод для проверки состояния "грязной" формы
        public bool IsDirty()
        {
            return _isDirty;
        }
    }
}
