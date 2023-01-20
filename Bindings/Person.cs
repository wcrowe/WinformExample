namespace Bindings
{
    public class PersonOld
    {
        private bool _isDirty = false;
        private string _firstName = null!;
        private string _lastName = null!;
        private int _age;
        private string _address = null!;
        private bool _isEmployed;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    _isDirty = true;
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    _isDirty = true;
                }
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    _isDirty = true;
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    _isDirty = true;
                }
            }
        }

        public bool IsEmployed
        {
            get { return _isEmployed; }
            set
            {
                if (_isEmployed != value)
                {
                    _isEmployed = value;
                    _isDirty = true;
                }
            }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public void Save()
        {
            // Code to save the model to a database or file
            _isDirty = false;
        }

        public void Reset()
        {
            _isDirty = false;
            _firstName = "";
            _lastName = "";
            _age = 0;
            _address = "";
            _isEmployed = false;
        }
    }

}