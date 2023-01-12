using WinformExample.Models;

namespace WinformExample {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            var testData = new List<Person> {
                new Person("John", "Doe", 30),
                new Person("Jane", "Smith", 25),
                new Person("Bob", "Johnson", 35),
                new Person("Samantha", "Williams", 27),
                new Person("Michael", "Brown", 40)
            };
        }

        private void Form1_Load(object sender, EventArgs e) { }
    }
}