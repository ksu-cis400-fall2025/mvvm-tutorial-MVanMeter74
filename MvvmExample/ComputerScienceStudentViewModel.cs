using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// student being represented
        /// </summary>
        public Student _student { get; init; }
        /// <summary>
        /// first name of student
        /// </summary>
        public string FirstName => _student.FirstName;
        /// <summary>
        /// last name of student
        /// </summary>
        public string LastName => _student.LastName;

        public IEnumerable<CourseRecord> ComputerScienceCourseRecords => _student.CourseRecords;
        /// <summary>
        /// grade point average of student
        /// </summary>
        public double GPA => _student.GPA;

        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                        
                }
                return points / hours;
            }
        }

        private void HandleStudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.GPA))
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
            if(e.PropertyName == nameof(Student.CourseRecords))
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceCourseRecords)));
            }

        }
        public ComputerScienceStudentViewModel(Student student)
        {
            _student = student;
            _student.PropertyChanged += HandleStudentPropertyChanged;
        }
    }
}
