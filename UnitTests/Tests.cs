using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BusinessLogic;
using DataLayer;
using System.Collections.ObjectModel;

namespace UnitTests
{
    [TestFixture]

   
    public class Tests
    {
        private DataConverter dataConverter;
        private PosDatabaseEntities ctx;
                
        [OneTimeSetUp]
        public void Init()
        {

            ctx = new PosDatabaseEntities();
            dataConverter = new DataConverter(ctx);
        }
    
        [TestCase]
        public void DatabaseConnectedTest()
        {
            Assert.IsInstanceOf(typeof(PosDatabaseEntities), ctx);
        }

        [TestCase]
        public void DbTermekToObservationCollectionTest()
        {
            ObservableCollection<Termek> termekek = new ObservableCollection<Termek>();
            var firstProduct = ctx.PRODUCTs.Select(x => x).First();
            Termek t = new Termek();
            t.TermekId = (int)firstProduct.PRODUCTID;
            t.Name = firstProduct.PNAME;
            t.Picture = firstProduct.PICTURE;
            t.Category = DataConverter.CategoryConverter(firstProduct.CATEGORY);
            t.Price = (int)firstProduct.UNITPRICE;
            termekek.Add(t);

            ObservableCollection<Termek> eredmeny = DataConverter.ProductListConverter(ctx.PRODUCTs.Where(x=>x.PRODUCTID==1).ToObservableCollection());
            Assert.That(eredmeny.First().Price, Is.EqualTo(termekek.First().Price));

        }

        [TestCase]
        public void CategoryConverterTest()
        {
            

            ///////////Input data///////////////////////////
            CATEGORY cat = new CATEGORY() {
                CATEGORYID=decimal.Parse("9"),
                CNAME="Feltét",
                PICTURE= "nincs"

            };
            ///////////////////////////////////////////////

            ///////////Excpected Result////////////////////
            Category exResult = new Category() {
                CategoryId=9,
                Name="Feltét",
                Picture="nincs"
            };
            ///////////////////////////////////////////////

            /////////////////////Assert/////////////////////
            var result = DataConverter.CategoryConverter(cat);
            Assert.That(result.Name, Is.EqualTo(exResult.Name));
            Assert.That(result.Picture, Is.EqualTo(exResult.Picture));
            Assert.That(result.CategoryId, Is.EqualTo(exResult.CategoryId));






        }

        [TestCase]
        public void OrderConverterTest()
        {
            //////////INPUT DATA/////////////
            Order basicOrder = new Order() {
                OrderId = 1,
                Rating = 5,
                TermekList = new ObservableCollection<OrderListItem>(),
                Total=2500
            };
            ///////////////////////////////////

            //////////////////Expected Result///////////////
            ORDER exResult = new ORDER() {
                ORDERID=1,
                RATING=5,
                TOTALPRICE=2500
            };
            ///////////////////////////////////////////

            //////////////Asserts///////////////////////
            var result = dataConverter.OrderConverter(basicOrder);
            Assert.That(result.ORDERID, Is.EqualTo(exResult.ORDERID));
            Assert.That(result.RATING, Is.EqualTo(exResult.RATING));
            Assert.That(result.TOTALPRICE, Is.EqualTo(exResult.TOTALPRICE));
        }

        [TestCase]
        public void ProductListConverter()
        {
            ObservableCollection<PRODUCT> dbProducts = ctx.PRODUCTs.Where(x => x.PRODUCTID == 1).ToObservableCollection();
            ObservableCollection<Termek> exRes = new ObservableCollection<Termek>();
            exRes.Add(new Termek() {
                TermekId =1,
                Name = "Cola",
                Picture = null,
                Category = null,
                Price = 350
            });
            var result = DataConverter.ProductListConverter(dbProducts);
            foreach (var item in result)
            {
                Assert.That(item.Name,Is.EqualTo(exRes.First().Name));
                Assert.That(item.Price,Is.EqualTo(exRes.First().Price));

            }

        }




    }
}
