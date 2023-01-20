using System.ComponentModel;
using Bindings.Models;
using Microsoft.EntityFrameworkCore;

namespace Bindings
{
    public class MyViewModel
    {
        private BindingList<Person> _models = null!;
        public BindingList<Person> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                _models.ListChanged += Models_ListChanged!;
            }
        }
        public void SortByProperty1()
        {
            var sortedModels = Models.OrderBy(m => m.FirstName).ToList();
            Models = new BindingList<Person>(sortedModels);
        }

        public void SortByProperty2()
        {
            var sortedModels = Models.OrderBy(m => m.FirstName).ThenBy(m => m.LastName).ToList();
            Models = new BindingList<Person>(sortedModels);
        }
        private void Models_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                Models[e.NewIndex].IsDirty = true;
            }
            else if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                Models[e.NewIndex].IsDirty = true;
            }
        }

        public void AddModel(Person model)
        {
            Models.Add(model);
        }

        public void DeleteModel(Person model)
        {
            Models.Remove(model);
        }

        public void SaveChanges()
        {
            using (var context = new MyDbContext())
            {
                var dirtyModels = Models.Where(m => m.IsDirty).ToList();
                var newModels = dirtyModels.Where(m => m.Id == 0).ToList();
                var updatedModels = dirtyModels.Where(m => m.Id != 0).ToList();
                var deletedModels = Models.Where(m => !dirtyModels.Contains(m)).ToList();

                context.MyEntities.AddRange(newModels);
                context.MyEntities.UpdateRange(updatedModels);
                context.MyEntities.RemoveRange(deletedModels);
                context.SaveChanges();
            }
        }
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Person> MyEntities { get; set; } = null!;
    }
}
