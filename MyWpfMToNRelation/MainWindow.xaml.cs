using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MyWpfMToNRelation.LocalTools;

namespace MyWpfMToNRelation
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region INotify Changed Properties  
        private string message;
        public string Message
        {
            get { return message; }
            set { SetField(ref message, value, nameof(Message)); }
        }
        private Course selectedCourse;
        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set { SetField(ref selectedCourse, value, nameof(SelectedCourse)); }
        }
        private ObservableCollection<Course> courses;
        public ObservableCollection<Course> Courses
        {
            get { return courses; }
            set { SetField(ref courses, value, nameof(Courses)); }
        }
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { SetField(ref selectedStudent, value, nameof(SelectedStudent)); }
        }
        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set { SetField(ref students, value, nameof(Students)); }
        }

        private ObservableCollection<Binding> bindings;
        public ObservableCollection<Binding> Bindings
        {
            get { return bindings; }
            set { SetField(ref bindings, value, nameof(Bindings)); }
        }

        // Template for a new INotify Changed Property
        // for using CTRL-R-R
        private bool xxx;
        public bool Xxx
        {
            get { return xxx; }
            set { SetField(ref xxx, value, nameof(Xxx)); }
        }
        #endregion

        SolidColorBrush _normalRowColor = Brushes.White;
        SolidColorBrush _assignedMcolor = Brushes.CornflowerBlue;

        BindingsWindow _bindingsWindow;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Courses = LST.PopulateCourses();
            Students = LST.PopulateStudents();
            Bindings = LST.PopulateBindings();

            _bindingsWindow = new BindingsWindow();
            _bindingsWindow.Bindings = Bindings;
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        #region Debug Buttons
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = "Debug Button #1: ";
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = "Debug Button #2: ";
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = "Debug Button #3: ";
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = "Debug Button #4: ";
        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = "Debug Button #5: ";

        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = "Debug Button #6: ";
        }
        #endregion

        /// <summary>
        /// Button_Reload_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Reload_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_Reload_Click");

            Courses = LST.PopulateCourses();
            Students = LST.PopulateStudents();
            Bindings = LST.PopulateBindings();
            _bindingsWindow.Bindings = Bindings;

            InitGridColor();
        }

        /// <summary>
        /// Button_Clear_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ResetMarkers_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_ResetMarkers_Click");

            SelectedCourse = null;
            SelectedStudent = null;
            ClearAllTags();
        }

        /// <summary>
        /// Button_Load_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_Load_Click");

            Courses = LST.LToO<Course>(LST.LoadList<Course>(AppDomain.CurrentDomain.BaseDirectory + "Courses.xml"));
            Students = LST.LToO<Student>(LST.LoadList<Student>(AppDomain.CurrentDomain.BaseDirectory + "Students.xml"));
            Bindings = LST.LToO<Binding>(LST.LoadList<Binding>(AppDomain.CurrentDomain.BaseDirectory + "Bindings.xml"));
            _bindingsWindow.Bindings = Bindings;

            Message = "We load Courses.xml, Students.xml and Bindings.xml";
        }

        /// <summary>
        /// Button_Save_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_Save_Click");

            LST.SaveList(LST.OToL<Course>(Courses), AppDomain.CurrentDomain.BaseDirectory + "Courses.xml");
            LST.SaveList(LST.OToL<Student>(Students), AppDomain.CurrentDomain.BaseDirectory + "Students.xml");
            LST.SaveList(LST.OToL<Binding>(Bindings), AppDomain.CurrentDomain.BaseDirectory + "Bindings.xml");

            Message = "We save Courses.xml, Students.xml and Bindings.xml";
        }
        
        /// <summary>
        /// Button_Bindings_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Bindings_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_Bindings_Click");

            // Set the BindingsWindow at the right 
            // site of the main window
            _bindingsWindow.Left = Left + Width;
            _bindingsWindow.Top = Top;
            _bindingsWindow.Show();
        }

        /// <summary>
        /// Button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// Window_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitGridColor();
        }

        /// <summary>
        /// DataGridCourses_MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (sender == dataGridCourses)
                {
                    Course course = GetDataGridRow(sender, e).Item as Course;
                    if (AssignRemoveCourse(sender, e))
                        ToogleRowColor(DataGridRow(Courses.IndexOf(course), dataGridCourses));
                }
                if (sender == dataGridStudents)
                {
                    Student student = GetDataGridRow(sender, e).Item as Student;
                    if (AssignRemoveStudent(sender, e))
                        ToogleRowColor(DataGridRow(Students.IndexOf(student), dataGridStudents));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in DataGrid_MouseDown: " + ex.ToString());
            }
        }
        private void ToogleRowColor(DataGridRow row)
        {
            if (row == null) { System.Console.Beep(); System.Console.Beep(); return; }

            if (GetColorOfRow(row) != _normalRowColor)
                SetColorRow(row, _normalRowColor);
            else
                SetColorRow(row, _assignedMcolor);
        }
        private Brush GetColorOfRow(DataGridRow row)
        {
            return row.Background;
        }
        private bool AssignRemoveStudent(object sender, MouseButtonEventArgs e)
        {
            Student student = GetDataGridRow(sender, e).Item as Student;
            if (student == null)
            {
                Console.Beep(); Console.Beep();
                Message = "ERROR You Must Click On A Student";
                return false;
            }
            if (SelectedCourse != null)
            {
                // everything ok here we go...
                if (BindingAlreadyExist(SelectedCourse, student))
                {
                    RemoveBinding(SelectedCourse.Id, student.Id);
                    Message = String.Format("We Delte Student {0} {1} {2} from Course {3} {4}", student.Id, student.FirstName, student.SurName, SelectedCourse.Id, SelectedCourse.Name);
                    return true;
                }
                if (sender == dataGridStudents)
                {
                    Bindings.Add(new Binding { Id = GetNexBindingId(Bindings), CourseId = SelectedCourse.Id, StudentId = student.Id });
                    Message = String.Format("We Assign Student {0} {1} {2} to Course {3} {4}", student.Id, student.FirstName, student.SurName, SelectedCourse.Id, SelectedCourse.Name);
                    return true;
                }
                else
                {
                    Console.Beep(); Console.Beep();
                    Message = "ERROR You Must Click On A Student";
                    return false;
                }
            }
            else
            {
                Console.Beep(); Console.Beep();
                Message = "ERROR No Course Is Selected";
                return false;
            }
        }
        private bool AssignRemoveCourse(object sender, MouseButtonEventArgs e)
        {
            Course course = GetDataGridRow(sender, e).Item as Course;
            if (course == null)
            {
                Console.Beep(); Console.Beep();
                Message = "ERROR You Must Click On A Course";
                return false;
            }
            if (SelectedStudent != null)
            {
                // everything ok here we go...
                if (BindingAlreadyExist(course, SelectedStudent))
                {
                    RemoveBinding(course.Id, SelectedStudent.Id);
                    Message = String.Format("We Delte Course {0} {1} from Student {2} {3} {4}", course.Id, course.Name, SelectedStudent.Id, SelectedStudent.FirstName, SelectedStudent.SurName);
                    return true;
                }
                if (sender == dataGridCourses)
                {
                    Bindings.Add(new Binding { Id = GetNexBindingId(Bindings), CourseId = course.Id, StudentId = SelectedStudent.Id });
                    Message = String.Format("We AssignCourse {0} {1} to Student {2} {3} {4}", course.Id, course.Name, SelectedStudent.Id, SelectedStudent.FirstName, SelectedStudent.SurName);
                    return true;
                }
                else
                {
                    Console.Beep(); Console.Beep();
                    Message = "ERROR You Must Click On A Course";
                    return false;
                }
            }
            else
            {
                Console.Beep(); Console.Beep();
                Message = "ERROR No Student Is Selected";
                return false;
            }
        }
        private DataGridRow GetDataGridRow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            return ItemsControl.ContainerFromElement((DataGrid)sender, (DependencyObject)e.OriginalSource) as DataGridRow;
        }
        private bool BindingAlreadyExist(Course course, Student student)
        {
            return Bindings.FirstOrDefault(b => b.StudentId == student.Id && b.CourseId == course.Id) == null ? false : true;
        }
        private void RemoveBinding(int courseId, int studentId)
        {
            Bindings.Remove(Bindings.FirstOrDefault(b => b.StudentId == studentId && b.CourseId == courseId));
        }
        private int GetNexBindingId(ObservableCollection<Binding> bindings)
        {
            if (bindings.Count == 0)
                return 1;
            else
                return bindings.Max(b => b.Id) + 1;
        }

        /// <summary>
        /// DataGridCourses_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(String.Format("We entered DataGridCourses_SelectionChanged"));

            if (SelectedCourse != null)
            {
                SelectedStudent = null;
                if (e.AddedItems.Count > 0)
                    MarkAllAssigendStudens(e.AddedItems[0] as Course);
            }
        }
        private void MarkAllAssigendStudens(Course course)
        {
            if (course == null) return;
            ClearAllTags();
            foreach (var s in Students)
                foreach (var b in Bindings)
                    if (course.Id == b.CourseId && s.Id == b.StudentId)
                        SetColorRow(DataGridRow(Students.IndexOf(s), dataGridStudents), _assignedMcolor);
        }

        /// <summary>
        /// DataGridStudents_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(String.Format("We entered DataGridStudents_SelectionChanged"));

            if (SelectedStudent != null)
            {
                SelectedCourse = null;
                if (e.AddedItems.Count > 0)
                    MarkAllAssigendCourses(e.AddedItems[0] as Student);
            }
        }
        private void MarkAllAssigendCourses(Student student)
        {
            if (student == null) return;
            ClearAllTags();
            foreach (var c in Courses)
                foreach (var b in Bindings)
                    if (student.Id == b.StudentId && c.Id == b.CourseId)
                        SetColorRow(DataGridRow(Courses.IndexOf(c), dataGridCourses), _assignedMcolor);
        }

        /// <summary>
        /// DataGridCourses_AddingNewItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCourses_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = new Course { Id = NextCourseId() };
        }
        private int NextCourseId()
        {
            if (Courses.Count == 0)
                return 1;
            else
                return Courses.Max(b => b.Id) + 1;
        }

        /// <summary>
        /// DataGridStudents_AddingNewItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridStudents_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = new Student { Id = NextStudentId() };
        }
        private int NextStudentId()
        {
            if (Students.Count == 0)
                return 1;
            else
                return Students.Max(b => b.Id) + 1;
        }

        /// <summary>
        /// DataGridCourses_UnloadingRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCourses_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            Course course = e.Row.Item as Course;
            RemoveCouseFromBinding(course);
        }
        private void RemoveCouseFromBinding(Course course)
        {
            var delist = Bindings.Where(b => b.CourseId == course.Id).ToList();
            foreach (var item in delist)
                Bindings.Remove(item);
        }

        /// <summary>
        /// DataGridStudents_UnloadingRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridStudents_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            Student student = e.Row.Item as Student;
            RemoveStudentFromBinding(student);
        }
        private void RemoveStudentFromBinding(Student student)
        {
            var delist = Bindings.Where(s => s.StudentId == student.Id).ToList();
            foreach (var item in delist)
                Bindings.Remove(item);
        }

        /// <summary>
        /// Lable_Message_MouseDown
        /// Clear Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lable_Message_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Message = "";
        }

        /// <summary>
        /// Window_Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _bindingsWindow.Close();
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// SetColorRow
        /// </summary>
        /// <param name="row"></param>
        /// <param name="color"></param>
        private void SetColorRow(DataGridRow row, SolidColorBrush color)
        {
            if (row != null) row.Background = color;
        }

        /// <summary>
        /// ClearAllTags
        /// </summary>
        private void ClearAllTags()
        {
            InitGridColor();
        }

        /// <summary>
        /// InitGridColor
        /// Initialization for all grids
        /// </summary>
        private void InitGridColor()
        {
            // This is necessary because if (GetColorOfRow(row) != Brushes.White)
            // does not work without initialization in dataGridCourses
            // see:
            // https://stackoverflow.com/questions/6713365/itemcontainergenerator-containerfromitem-returns-null
            dataGridCourses.UpdateLayout();
            for (int i = 0; i < dataGridCourses.Items.Count - 1; i++)
                SetColorRowByIndex(i, _normalRowColor, dataGridCourses, Courses);

            dataGridStudents.UpdateLayout();
            for (int i = 0; i < dataGridStudents.Items.Count - 1; i++)
                SetColorRowByIndex(i, _normalRowColor, dataGridStudents, Students);
        }

        /// <summary>
        /// SetColorRowByIndex
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowNumber"></param>
        /// <param name="color"></param>
        /// <param name="dataGrid"></param>
        /// <param name="bindingList"></param>
        private void SetColorRowByIndex<T>(int rowNumber, SolidColorBrush color, DataGrid dataGrid, ObservableCollection<T> bindingList)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(bindingList[rowNumber]);
            if (row != null) row.Background = color;
        }

        /// <summary>
        /// DataGridRow
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        private DataGridRow DataGridRow(int index,DataGrid dataGrid)
        {
            return (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
        }

        /// <summary>
        /// SetField
        /// for INotify Changed Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }

    public class Course
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
    }

    public class Binding
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }
}
