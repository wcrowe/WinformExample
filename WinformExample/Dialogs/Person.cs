using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformExample.Models;

namespace WinformExample.Dialogs;
using System;
using System.Windows.Forms;

class PersonDialog : Form
{
    private TextBox firstNameTextBox;
    private TextBox lastNameTextBox;
    private TextBox ageTextBox;
    private Button addButton;
    private Button updateButton;
    private Button deleteButton;
    private ListBox personListBox;

    private List<Person> people;
    private Person selectedPerson;

    public PersonDialog()
    {
        // Initialize form controls
        firstNameTextBox = new TextBox();
        lastNameTextBox = new TextBox();
        ageTextBox = new TextBox();
        addButton = new Button();
        updateButton = new Button();
        deleteButton = new Button();
        personListBox = new ListBox();

        // Set up add button
        addButton.Text = "Add";
        addButton.Click += AddButton_Click;

        // Set up update button
        updateButton.Text = "Update";
        updateButton.Click += UpdateButton_Click;
        updateButton.Enabled = false;

        // Set up delete button
        deleteButton.Text = "Delete";
        deleteButton.Click += DeleteButton_Click;
        deleteButton.Enabled = false;

        // Set up person list box
        personListBox.SelectedIndexChanged += PersonListBox_SelectedIndexChanged;

        // Add controls to form
        this.Controls.Add(firstNameTextBox);
        this.Controls.Add(lastNameTextBox);
        this.Controls.Add(ageTextBox);
        this.Controls.Add(addButton);
        this.Controls.Add(updateButton);
        this.Controls.Add(deleteButton);
        this.Controls.Add(personListBox);

        // Position controls on form
        // ...

        // Set up form
        this.Text = "Person Dialog";
        this.people = new List<Person>();
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        string firstName = firstNameTextBox.Text;
        string lastName = lastNameTextBox.Text;
        int age = int.Parse(ageTextBox.Text);

        Person person = new Person(firstName, lastName, age);
        people.Add(person);

        personListBox.Items.Add(person);

        ClearInputFields();
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
        selectedPerson.FirstName = firstNameTextBox.Text;
        selectedPerson.LastName = lastNameTextBox.Text;
        selectedPerson.Age = int.Parse(ageTextBox.Text);

        int selectedIndex = personListBox.SelectedIndex;
        personListBox.Items[selectedIndex] = selectedPerson;
        personListBox.SelectedIndex = -1;

        ClearInputFields();
        updateButton.Enabled = false;
        deleteButton.Enabled = false;
    }


    private void DeleteButton_Click(object sender, EventArgs e)
    {
        int selectedIndex = personListBox.SelectedIndex;
        people.RemoveAt(selectedIndex);
        personListBox.Items.RemoveAt(selectedIndex);

        ClearInputFields();
        updateButton.Enabled = false;
        deleteButton.Enabled = false;
    }

    private void PersonListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedPerson = (Person)personListBox.SelectedItem;
        if (selectedPerson != null)
        {
            firstNameTextBox.Text = selectedPerson.FirstName;
            lastNameTextBox.Text = selectedPerson.LastName;
            ageTextBox.Text = selectedPerson.Age.ToString();
            updateButton.Enabled = true;
            deleteButton.Enabled = true;
        }
    }

    private void ClearInputFields()
    {
        firstNameTextBox.Clear();
        lastNameTextBox.Clear();
        ageTextBox.Clear();
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // PersonDialog
            // 
            this.ClientSize = new System.Drawing.Size(581, 395);
            this.Name = "PersonDialog";
            this.ResumeLayout(false);

    }
}
