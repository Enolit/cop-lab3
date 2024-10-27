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
            // ����������� ���������� ��������� ��� ��������� Windows-1252
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            _context = new StudentContext();
            LoadStudents();

            // ������������� ������������ ����
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            // �������� ��������� ����
            ToolStripMenuItem addItem = new ToolStripMenuItem("�������� ������", null, AddItem_Click);
            ToolStripMenuItem editItem = new ToolStripMenuItem("������������� ������", null, EditItem_Click);
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("������� ������", null, DeleteItem_Click);
            ToolStripMenuItem simpleDocItem = new ToolStripMenuItem("������� ������� ��������", null, SimpleDocItem_Click);
            ToolStripMenuItem docTableItem = new ToolStripMenuItem("������� �������� � ��������", null, DocTableItem_Click);
            ToolStripMenuItem docChartItem = new ToolStripMenuItem("������� �������� � ����������", null, DocChartItem_Click);

            // ���������� ��������� � ����������� ����
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addItem, editItem, deleteItem, simpleDocItem, docTableItem, docChartItem });

            // �������� ������������ ���� � ����������
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
            // ������� ����� ����� ���������� ��������
            var studentForm = new StudentForm(_context);

            // ������������� �� ������� FormClosing ��� �������� ������������� ���������
            studentForm.FormClosing += (s, e) =>
            {
                if (studentForm.IsDirty())
                {
                    var result = MessageBox.Show("���� ������������� ���������. �� �������, ��� ������ ������� �����?",
                        "�������������", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true; // ������ �������� �����
                    }
                }
            };

            // ���������� ����� ��� ��������� ����
            if (studentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents(); // ��������� ������ ��������� ����� ����������
            }
        }

        private void EditStudent()
        {
            // ��������� ���������� ���� �� ����������
            var selectedNode = controlDataTreeTable1.GetSelectedObject<StudentDisplay>();

            if (selectedNode == null)
            {
                MessageBox.Show("����������, �������� ������ ��� ��������������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ������� �������� �� ID
            var studentToEdit = _context.Students.Find(selectedNode.Id);
            if (studentToEdit == null)
            {
                MessageBox.Show("������� �� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ������� ����� �������������� �������� � �������� ������������ ������
            var studentForm = new StudentForm(_context, studentToEdit);

            // ������������� �� ������� FormClosing ��� �������� ������������� ���������
            studentForm.FormClosing += (s, e) =>
            {
                if (studentForm.IsDirty())
                {
                    var result = MessageBox.Show("���� ������������� ���������. �� �������, ��� ������ ������� �����?",
                        "�������������", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true; // ������ �������� �����
                    }
                }
            };

            // ���������� ����� ��� ��������� ����
            if (studentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents(); // ��������� ������ ��������� ����� ��������������
            }
        }


        private void DeleteStudent()
        {
            // ��������� ���������� ���� �� ����������
            var selectedNode = controlDataTreeTable1.GetSelectedObject<StudentDisplay>();
            
            if (selectedNode == null)
            {
                MessageBox.Show("����������, �������� ������ ��� ��������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ������������� ��������
            var confirmationResult = MessageBox.Show(
                $"�� �������, ��� ������ ������� ������: {selectedNode.FullName}?",
                "������������� ��������",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmationResult == DialogResult.Yes)
            {
                // ��������� �������� ������ �� ���� ������
                var studentId = selectedNode.Id; // �������� ID ��������
                var studentToDelete = _context.Students.Find(studentId);

                if (studentToDelete != null)
                {
                    _context.Students.Remove(studentToDelete);
                    _context.SaveChanges(); // ��������� ��������� � ���� ������

                    MessageBox.Show("������ ������� �������.", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudents(); // ��������� ����������� ������
                }
                else
                {
                    MessageBox.Show("������ �� �������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CreateDoc() 
        {
            // ������ ���� �� ����� � ����� ����� � ������������
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word Document|*.docx"; // ��������� ������� ��� ������� �����
                saveFileDialog.Title = "��������� ��������";
                saveFileDialog.FileName = "��������������������"; // ��� ����� �� ���������
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // ��������� ����������

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName; // �������� ������ ���� � �����

                   // �������� ��������� �� ����������
                    var studentsWithScholarship = _context.Students.Where(s => s.Scholarship > 0).ToList();

                    // ������� ������ ���������� ��� ���������
                    List<ParagraphData> paragraphs = new List<ParagraphData>();
                    foreach (var student in studentsWithScholarship)
                    {
                        // ����������� ������ � ����������� � ��������
                        string studentInfo = $"{student.FullName}: {student.Description}";
                        paragraphs.Add(new ParagraphData(studentInfo));
                    }
                    // ������� ��������
                    bigTextComponent1.CreateDocument(filePath, "�������� �� ����������", paragraphs);

                    // ������� ��������� �� �������� �������� ���������
                    MessageBox.Show($"�������� ������� ������: {filePath}", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            } 
        }

        private void CreateTable()
        {
            // ��������� ������ ��� ������ ���� � ����� �����
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF ����� (*.pdf)|*.pdf";
                saveFileDialog.Title = "��������� �����";
                saveFileDialog.FileName = "����������������"; // ��� ����� �� ���������
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // ��������� ����������


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // �������� ������ ���� ���������
                    var students = _context.Students.OrderBy(s => s.Id).ToList();

                    // ����������� ������������ �������
                    var config = new ComponentDocumentWithTableHeaderDataConfig<Student>
                    {
                        FilePath = saveFileDialog.FileName, // ���� ��� ���������� �����
                        Header = "����� �� ���� ���������", // ��������� ���������
                        Data = students, // ������ ���������
                        ColumnsRowsWidth = new List<(int Column, int Row)>
                        {
                            (20, 10), // ������ ������� ��� ID
                            (40, 10), // ������ ������� ��� ���
                            (20, 10), // ������ ������� ��� �����
                            (20, 10)  // ������ ������� ��� ���������
                        },
                        Headers = new List<(int ColumnIndex, int RowIndex, string Header, string PropertyName)>
                        {
                            (0, 0, "ID", "Id"),                  // ��������� ��� ID
                            (1, 0, "���", "FullName"),           // ��������� ��� ���
                            (2, 0, "����", "Course"),            // ��������� ��� ����
                            (3, 0, "���������", "Scholarship") // ��������� ��� ���������
                        }
                    };

                    // ������� �������� PDF � ��������
                    componentTablePdf1.CreateDoc(config);

                    // ���������� ���������System.ArgumentOutOfRangeException: "Specified argument was out of the range of valid values. Arg_ParamName_Name"��� �� �������� �������� ���������
                    MessageBox.Show("������� ������� �������", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void CreateChart()
        {
            // ��������� ������ ��� ������ ���� � ����� �����
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel ����� (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "��������� ���������";
                saveFileDialog.FileName = "���������������������"; // ��� ����� �� ���������
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // ��������� ����������


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // �������� ������ ���� ���������
                    var students = _context.Students.ToList();

                    // ������� ��������� �� ������ �����, ���������� ���������
                    var scholarshipCounts = students
                        .Where(s => s.Scholarship > 0) // ��������� ���������, ���������� ���������
                        .GroupBy(s => s.Course) // ���������� �� �����
                        .ToDictionary(g => g.Key, g => g.Count()); // ������� ������� � ������� � �� �����������


                    // ����������� ������������ �������
                    var chartConfig = new ComponentDocumentWithChartConfig
                    {
                        FilePath = saveFileDialog.FileName, // ���� ��� ���������� �����
                        Header = "�������� ���������: ��������� ���������",
                        ChartTitle = "���������� ��������� �� ���������� �� ������",

                        LegendLocation = ComponentsLibraryNet60.Models.Location.Bottom, // ���������, ��� ����� ������������� �������
                        Data = new Dictionary<string, List<(int Date, double Value)>>
                        {
                            {
                                "���������", scholarshipCounts
                                .Select(kv => ((int)kv.Key, (double)kv.Value))
                                .ToList() 
                            } 
                        }
                    };

                    // ������� �������� PDF � ��������
                    componentChartPieExcel1.CreateDoc(chartConfig);

                    // ���������� ���������System.ArgumentOutOfRangeException: "Specified argument was out of the range of valid values. Arg_ParamName_Name"��� �� �������� �������� ���������
                    MessageBox.Show("��������� ������� �������", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadStudents()
        {
            var students = _context.Students.ToList();

            // ���������� ��������� �� ����� � �������� ���������
            var courseGroups = students
                .GroupBy(s => s.Course)
                .Select(courseGroup => new
                {
                    Course = courseGroup.Key,
                    Scholarships = courseGroup
                        .GroupBy(s => s.Scholarship)  // ���������� �� �������� ���������
                        .Select(scholarshipGroup => new
                        {
                            ScholarshipAmount = scholarshipGroup.Key,  // �������� ���������
                            Students = scholarshipGroup.ToList()
                        })
                })
                .ToList();

            // �������� ������������ �����
            var nodeConfig = new ControlsLibraryNet60.Models.DataTreeNodeConfig
            {
                NodeNames = new Queue<string>(new[]
                {
                    "Course",      // ������� 1: ����
                    "Scholarship", // ������� 2: ���������
                    "Id",          // ������� 3: �������������
                    "FullName"     // ������� 4: ���
                })
            };

            // ������� ������ ��� �������� � ���������
            var dataToDisplay = new List<StudentDisplay>();

            foreach (var courseGroup in courseGroups)
            {
                foreach (var scholarshipGroup in courseGroup.Scholarships)
                {
                    foreach (var student in scholarshipGroup.Students)
                    {
                        dataToDisplay.Add(new StudentDisplay
                        {
                            Course = $"����: {courseGroup.Course}",
                            Scholarship = $"���������: {scholarshipGroup.ScholarshipAmount}",
                            Id = student.Id,
                            FullName = student.FullName
                        });

                    }
                }
            }

            // ��������� ������������ � ���������
            controlDataTreeTable1.LoadConfig(nodeConfig);

            // ������� ������ ������ ����� ����������� �����
            controlDataTreeTable1.Clear();

            // ��������� ������ � ���������
            controlDataTreeTable1.AddTable(dataToDisplay);
        }
    }
}
