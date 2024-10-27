using ComponentsLibraryNet60.Models;
using ControlsLibraryNet60.Core;
using LabLibrary2;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp3
{

    public partial class Form1 : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.A:
                    AddStudent();
                    return true;
                case Keys.Control | Keys.U:
                    EditStudent();
                    return true;
                case Keys.Control | Keys.D:
                    DeleteStudent();
                    return true;
                case Keys.Control | Keys.S:
                    CreateDoc();
                    return true;
                case Keys.Control | Keys.T:
                    CreateTable();
                    return true;
                case Keys.Control | Keys.C:
                    CreateChart();
                    return true;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private StudentContext _context;

        public Form1()
        {
            InitializeComponent();
            // Регистрация провайдера кодировок для поддержки Windows-1252
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            _context = new StudentContext();
            LoadStudents();

            // Инициализация контекстного меню
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            // Создание элементов меню
            ToolStripMenuItem addItem = new ToolStripMenuItem("Добавить запись", null, AddItem_Click);
            ToolStripMenuItem editItem = new ToolStripMenuItem("Редактировать запись", null, EditItem_Click);
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Удалить запись", null, DeleteItem_Click);
            ToolStripMenuItem simpleDocItem = new ToolStripMenuItem("Создать простой документ", null, SimpleDocItem_Click);
            ToolStripMenuItem docTableItem = new ToolStripMenuItem("Создать документ с таблицей", null, DocTableItem_Click);
            ToolStripMenuItem docChartItem = new ToolStripMenuItem("Создать документ с диаграммой", null, DocChartItem_Click);

            // Добавление элементов в контекстное меню
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addItem, editItem, deleteItem, simpleDocItem, docTableItem, docChartItem });

            // Привязка контекстного меню к компоненту
            controlDataTreeTable1.ContextMenuStrip = contextMenuStrip1;
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            AddStudent();
        }

        private void EditItem_Click(object sender, EventArgs e)
        {
            EditStudent();
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }

        private void SimpleDocItem_Click(object sender, EventArgs e) 
        {
            CreateDoc();
        }

        private void DocTableItem_Click(object sender, EventArgs e)
        {
            CreateTable();
        }

        private void DocChartItem_Click(object sender, EventArgs e)
        {
            CreateChart();
        }

        private void AddStudent()
        {
            // Создаем новую форму добавления студента
            var studentForm = new StudentForm(_context);

            // Подписываемся на событие FormClosing для проверки несохраненных изменений
            studentForm.FormClosing += (s, e) =>
            {
                if (studentForm.IsDirty())
                {
                    var result = MessageBox.Show("Есть несохраненные изменения. Вы уверены, что хотите закрыть форму?",
                        "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true; // Отмена закрытия формы
                    }
                }
            };

            // Показываем форму как модальное окно
            if (studentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents(); // Обновляем список студентов после добавления
            }
        }

        private void EditStudent()
        {
            // Получение выбранного узла из компонента
            var selectedNode = controlDataTreeTable1.GetSelectedObject<StudentDisplay>();

            if (selectedNode == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Находим студента по ID
            var studentToEdit = _context.Students.Find(selectedNode.Id);
            if (studentToEdit == null)
            {
                MessageBox.Show("Студент не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Создаем форму редактирования студента и передаем существующий объект
            var studentForm = new StudentForm(_context, studentToEdit);

            // Подписываемся на событие FormClosing для проверки несохраненных изменений
            studentForm.FormClosing += (s, e) =>
            {
                if (studentForm.IsDirty())
                {
                    var result = MessageBox.Show("Есть несохраненные изменения. Вы уверены, что хотите закрыть форму?",
                        "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true; // Отмена закрытия формы
                    }
                }
            };

            // Показываем форму как модальное окно
            if (studentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents(); // Обновляем список студентов после редактирования
            }
        }


        private void DeleteStudent()
        {
            // Получение выбранного узла из компонента
            var selectedNode = controlDataTreeTable1.GetSelectedObject<StudentDisplay>();
            
            if (selectedNode == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Подтверждение удаления
            var confirmationResult = MessageBox.Show(
                $"Вы уверены, что хотите удалить запись: {selectedNode.FullName}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmationResult == DialogResult.Yes)
            {
                // Выполняем удаление записи из базы данных
                var studentId = selectedNode.Id; // Получаем ID студента
                var studentToDelete = _context.Students.Find(studentId);

                if (studentToDelete != null)
                {
                    _context.Students.Remove(studentToDelete);
                    _context.SaveChanges(); // Сохраняем изменения в базе данных

                    MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents(); // Обновляем отображение данных
                }
                else
                {
                    MessageBox.Show("Запись не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CreateDoc() 
        {
            // Запрос пути до папки и имени файла у пользователя
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word Document|*.docx"; // Установка фильтра для формата файла
                saveFileDialog.Title = "Сохранить документ";
                saveFileDialog.FileName = "СтудентыСоСтипендией"; // Имя файла по умолчанию
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Начальная директория

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName; // Получаем полный путь к файлу

                   // Получаем студентов со стипендией
                    var studentsWithScholarship = _context.Students.Where(s => s.Scholarship > 0).ToList();

                    // Создаем список параграфов для документа
                    List<ParagraphData> paragraphs = new List<ParagraphData>();
                    foreach (var student in studentsWithScholarship)
                    {
                        // Форматируем строку с информацией о студенте
                        string studentInfo = $"{student.FullName}: {student.Description}";
                        paragraphs.Add(new ParagraphData(studentInfo));
                    }
                    // Создаем документ
                    bigTextComponent1.CreateDocument(filePath, "Студенты со стипендией", paragraphs);

                    // Выводим сообщение об успешном создании документа
                    MessageBox.Show($"Документ успешно создан: {filePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            } 
        }

        private void CreateTable()
        {
            // Открываем диалог для выбора пути и имени файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";
                saveFileDialog.Title = "Сохранить отчет";
                saveFileDialog.FileName = "ТаблицаСтудентов"; // Имя файла по умолчанию
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Начальная директория


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Получаем список всех студентов
                    var students = _context.Students.OrderBy(s => s.Id).ToList();

                    // Настраиваем конфигурацию таблицы
                    var config = new ComponentDocumentWithTableHeaderDataConfig<Student>
                    {
                        FilePath = saveFileDialog.FileName, // Путь для сохранения файла
                        Header = "Отчет по всем студентам", // Заголовок документа
                        Data = students, // Список студентов
                        ColumnsRowsWidth = new List<(int Column, int Row)>
                        {
                            (20, 10), // Ширина колонки для ID
                            (40, 10), // Ширина колонки для ФИО
                            (20, 10), // Ширина колонки для Курса
                            (20, 10)  // Ширина колонки для Стипендии
                        },
                        Headers = new List<(int ColumnIndex, int RowIndex, string Header, string PropertyName)>
                        {
                            (0, 0, "ID", "Id"),                  // Заголовок для ID
                            (1, 0, "ФИО", "FullName"),           // Заголовок для ФИО
                            (2, 0, "Курс", "Course"),            // Заголовок для Курс
                            (3, 0, "Стипендия", "Scholarship") // Заголовок для Стипендии
                        }
                    };

                    // Создаем документ PDF с таблицей
                    componentTablePdf1.CreateDoc(config);

                    // Оповещение пользоватSystem.ArgumentOutOfRangeException: "Specified argument was out of the range of valid values. Arg_ParamName_Name"еля об успешном создании документа
                    MessageBox.Show("Таблица успешно создана", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void CreateChart()
        {
            // Открываем диалог для выбора пути и имени файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel файлы (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Сохранить диаграмму";
                saveFileDialog.FileName = "ДиаграммаДляСтудентов"; // Имя файла по умолчанию
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Начальная директория


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Получаем список всех студентов
                    var students = _context.Students.ToList();

                    // Подсчет студентов на каждом курсе, получающих стипендию
                    var scholarshipCounts = students
                        .Where(s => s.Scholarship > 0) // Фильтруем студентов, получающих стипендию
                        .GroupBy(s => s.Course) // Группируем по курсу
                        .ToDictionary(g => g.Key, g => g.Count()); // Создаем словарь с курсами и их количеством


                    // Настраиваем конфигурацию таблицы
                    var chartConfig = new ComponentDocumentWithChartConfig
                    {
                        FilePath = saveFileDialog.FileName, // Путь для сохранения файла
                        Header = "Круговая диаграмма: Стипендия студентов",
                        ChartTitle = "Количество студентов со стипендией по курсам",

                        LegendLocation = ComponentsLibraryNet60.Models.Location.Bottom, // Указываем, где будет располагаться легенда
                        Data = new Dictionary<string, List<(int Date, double Value)>>
                        {
                            {
                                "Стипендия", scholarshipCounts
                                .Select(kv => ((int)kv.Key, (double)kv.Value))
                                .ToList() 
                            } 
                        }
                    };

                    // Создаем документ PDF с таблицей
                    componentChartPieExcel1.CreateDoc(chartConfig);

                    // Оповещение пользоватSystem.ArgumentOutOfRangeException: "Specified argument was out of the range of valid values. Arg_ParamName_Name"еля об успешном создании документа
                    MessageBox.Show("Диаграмма успешно создана", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadStudents()
        {
            var students = _context.Students.ToList();

            // Группируем студентов по курсу и значению стипендии
            var courseGroups = students
                .GroupBy(s => s.Course)
                .Select(courseGroup => new
                {
                    Course = courseGroup.Key,
                    Scholarships = courseGroup
                        .GroupBy(s => s.Scholarship)  // Группируем по значению стипендии
                        .Select(scholarshipGroup => new
                        {
                            ScholarshipAmount = scholarshipGroup.Key,  // Значение стипендии
                            Students = scholarshipGroup.ToList()
                        })
                })
                .ToList();

            // Создание конфигурации узлов
            var nodeConfig = new ControlsLibraryNet60.Models.DataTreeNodeConfig
            {
                NodeNames = new Queue<string>(new[]
                {
                    "Course",      // Уровень 1: Курс
                    "Scholarship", // Уровень 2: Стипендия
                    "Id",          // Уровень 3: Идентификатор
                    "FullName"     // Уровень 4: ФИО
                })
            };

            // Создаем список для передачи в компонент
            var dataToDisplay = new List<StudentDisplay>();

            foreach (var courseGroup in courseGroups)
            {
                foreach (var scholarshipGroup in courseGroup.Scholarships)
                {
                    foreach (var student in scholarshipGroup.Students)
                    {
                        dataToDisplay.Add(new StudentDisplay
                        {
                            Course = $"Курс: {courseGroup.Course}",
                            Scholarship = $"Стипендия: {scholarshipGroup.ScholarshipAmount}",
                            Id = student.Id,
                            FullName = student.FullName
                        });

                    }
                }
            }

            // Загружаем конфигурацию в компонент
            controlDataTreeTable1.LoadConfig(nodeConfig);

            // Очистка старых данных перед добавлением новых
            controlDataTreeTable1.Clear();

            // Добавляем данные в компонент
            controlDataTreeTable1.AddTable(dataToDisplay);
        }
    }
}
