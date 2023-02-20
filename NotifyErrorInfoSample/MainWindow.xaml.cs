using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace NotifyErrorInfoSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CustomerViewModel {  };
        }
    }

    public class CustomerViewModel : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool HasErrors => true;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public IEnumerable GetErrors(string? propertyName) => new List<string> { "Something is wrong!" };


        private string? _firstName = null;
        public string? FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                if (_firstName == string.Empty)
                {
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(FirstName)));
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }
    }


    //public class ModelBase : ValidationBase, INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected void NotifyPropertyChanged(string propertyName) => 
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}


    //public class Customer : ModelBase
    //{
    //    private string _firstName;
    //    [Display(Name = "First Name")]
    //    [Required]
    //    [StringLength(20)]
    //    public string FirstName
    //    {
    //        get { return _firstName; }
    //        set
    //        {
    //            _firstName = value;
    //            ValidateProperty(value);
    //            NotifyPropertyChanged(nameof(FirstName));
    //        }
    //    }
    //}

  
    //public class ValidationBasex : INotifyDataErrorInfo
    //{
    //    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    //    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


    //    private object _lock = new ();
    //    public bool HasErrors => _errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0);
    //    public bool IsValid => HasErrors;

    //    public IEnumerable GetErrors(string propertyName)
    //    {
    //        if (!string.IsNullOrEmpty(propertyName))
    //        {
    //            if (_errors.ContainsKey(propertyName) && (_errors[propertyName] != null) && _errors[propertyName].Count > 0)
    //                return _errors[propertyName].ToList();
    //            else
    //                return null;
    //        }
    //        else
    //            return _errors.SelectMany(err => err.Value.ToList());
    //    }

    //    public void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    //    public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
    //    {
    //        lock (_lock)
    //        {
    //            var validationContext = new ValidationContext(this, null, null)
    //            {
    //                MemberName = propertyName
    //            };
    //            var validationResults = new List<ValidationResult>();
    //            Validator.TryValidateProperty(value, validationContext, validationResults);

    //            //clear previous _errors from tested property  
    //            _errors.Remove(propertyName);
    //            OnErrorsChanged(propertyName);
    //            HandleValidationResults(validationResults);
    //        }
    //    }

    //    public void Validate()
    //    {
    //        lock (_lock)
    //        {
    //            var validationContext = new ValidationContext(this, null, null);
    //            var validationResults = new List<ValidationResult>();
    //            Validator.TryValidateObject(this, validationContext, validationResults, true);

    //            //clear all previous _errors  
    //            var propNames = _errors.Keys.ToList();
    //            _errors.Clear();
    //            propNames.ForEach(pn => OnErrorsChanged(pn));
    //            HandleValidationResults(validationResults);
    //        }
    //    }

    //    private void HandleValidationResults(List<ValidationResult> validationResults)
    //    {
    //        //Group validation results by property names  
    //        var resultsByPropNames = from res in validationResults
    //                                 from mname in res.MemberNames
    //                                 group res by mname into g
    //                                 select g;
    //        //add _errors to dictionary and inform binding engine about _errors  
    //        foreach (var prop in resultsByPropNames)
    //        {
    //            var messages = prop.Select(r => r.ErrorMessage).ToList();
    //            _errors.Add(prop.Key, messages);
    //            OnErrorsChanged(prop.Key);
    //        }
    //    }
    //}
}
