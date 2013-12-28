namespace AngularJsWebApp.Migrations
{
    using AngularJsWebApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularJsWebApp.Models.ToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngularJsWebApp.Models.ToDoContext context)
        {
            var r = new Random();

            var items = Enumerable.Range(1, 100).Select(o => new ToDo
            {
                DueDate = new DateTime(2012, r.Next(1, 12), r.Next(1, 28)),
                Priority = (byte)r.Next(10),
                Text = o.ToString()
            }).ToArray();

            context.ToDoes.AddOrUpdate(item => new { item.Text }, items);
        }
    }
}
