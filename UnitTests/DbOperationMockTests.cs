using BusinessLogic;
using DataLayer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    class DbOperationMockTests
    {
        private Mock<PosDatabaseEntities> mockedPos;
        private CategoryRepository catRepo;
        private PosDatabaseEntities db;

        [OneTimeSetUp]
        public void Init()
        {
            db = new PosDatabaseEntities();
            mockedPos = CreateMockedObject();
            catRepo = new CategoryRepository(mockedPos.Object);
        }


        [TestCase]
        public void UpdateCategoryTest()
        {
            CATEGORY catIns = new CATEGORY();
            catIns.CATEGORYID = 1;
            catIns.CNAME = "MM";

            catRepo.Update(catIns);
            Assert.That(mockedPos.Object.CATEGORies.Where(x => x.CATEGORYID == 1).First().CNAME, Is.EqualTo("MM"));
        }

        [TestCase]
        public void InsertDb()
        {
            CategoryRepository repo = new CategoryRepository(db);

            CATEGORY insertItem = new CATEGORY()
            {
                CATEGORYID = 100,
                CNAME = "Hello",
                PICTURE = "bb"
            };
            repo.Insert(insertItem);

            ObservableCollection<Category> catRes = DataConverter.CategoryListConverter(db.CATEGORies.Select(x => x).ToObservableCollection());
            ;
            Assert.That(catRes.Where(x => x.Name == "Hello").First().Name, Is.EqualTo(insertItem.CNAME));

        }

        [TestCase]
        public void DeleteDB()
        {
            ProductRepository repo = new ProductRepository(db);
            ObservableCollection<Termek> catBefore = DataConverter.ProductListConverter(db.PRODUCTs.Select(x => x).ToObservableCollection());




            repo.Delete(db.PRODUCTs.SingleOrDefault(x => x.PRODUCTID == 10));
            ObservableCollection<Termek> catRes = DataConverter.ProductListConverter(db.PRODUCTs.Select(x => x).ToObservableCollection());
            Assert.That(catRes.Count(), Is.EqualTo(catBefore.Count() - 1));
        }

        public Mock<PosDatabaseEntities> CreateMockedObject()
        {
            Mock<PosDatabaseEntities> mock = new Mock<PosDatabaseEntities>();
            List<CATEGORY> cat = new List<CATEGORY>() {
                new CATEGORY(){
                    CATEGORYID=1,
                    CNAME="Hello",
                    PICTURE="bb"
                },
                new CATEGORY(){
                    CATEGORYID=2,
                    CNAME="ASD",
                    PICTURE="aa"
                }
            };
            mock.Setup(x => x.CATEGORies).ReturnsDbSet(cat.AsQueryable());
            mock.Setup(x => x.Set<CATEGORY>()).ReturnsDbSet(cat.AsQueryable());
            return mock;
        }
    }
}
