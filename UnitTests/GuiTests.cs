using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Szt2;
using Szt2.ViewModels;
using DataLayer;
using System.Collections.ObjectModel;
using BusinessLogic;

namespace UnitTests
{
    [TestFixture]
    class GuiTests
    {
        private ViewModel mainVm;

        [OneTimeSetUp]
        public void Init()
        {
            mainVm = new ViewModel();
        }


        [TestCase]
        public void GuiDatabaseConnectedTest()
        {            
            Assert.NotNull(mainVm.Ctx);
        }


        [TestCase]
        public void ReadCategoryListFromDatabaseTest()
        {
            PosDatabaseEntities e = new PosDatabaseEntities();
            ObservableCollection<Category> expected = DataConverter.CategoryListConverter(e.CATEGORies.Select(x => x).ToObservableCollection());

            Assert.That(mainVm.CategoryList.ElementAt(1).CategoryId, Is.EqualTo(expected.ElementAt(1).CategoryId));

        }



        [TestCase]
        public void StringToCategoryListTest()
        {
            Category ex = new Category("Leves");
            Category res = mainVm.StringToCategory("Leves");

            Assert.That(res.Name, Is.EqualTo(ex.Name));
        }

        [TestCase]
        public void StringToCategoryListTestShouldBeNull()
        {
            Category c = mainVm.StringToCategory("fsdfd");
            Assert.IsNull(c);


        }


        [TestCase]
        public void ViewModelCategoryListIsNotNull()
        {
            ViewModel vm = new ViewModel();
            Assert.IsNotNull(vm.CategoryList);
        }


        [TestCase]
        public void ViewModelOrderIsNotNull()
        {
            ViewModel vm = new ViewModel();
            Assert.IsNotNull(vm.Order);
        }

    }
}
