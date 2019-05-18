using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BusinessLogic;
using DataLayer;
using System.Data.Entity;
using Moq;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Threading;

namespace UnitTests
{

    [TestFixture]
    public class StatisticMockTests
    {
        private Mock<PosDatabaseEntities> mockctx;
        private Statistics stat;

        [OneTimeSetUp]
        public void Init()
        {

            stat = new Statistics(2);
            
            mockctx = CreateMockObjectForIncomeSum();
        }

        [TestCase]
        public void IncomeSumTestMockShouldeBeZero()
        {
            Mock<PosDatabaseEntities> ct = CreateMockObjectForIncomeSum();
            int res = stat.CalcIncome(new DateTime(1998, 1, 1).Date, new DateTime(1999, 1, 1).Date, ct.Object);
            Assert.That(res, Is.EqualTo(0));
        }
        [TestCase]
        public void IncomeSumTestMockShouldeBe2900()
        {
            Mock<PosDatabaseEntities> ct = CreateMockObjectForIncomeSum();
            int res = stat.CalcIncome(new DateTime(1998, 1, 1).Date, new DateTime(2011, 1, 1).Date, ct.Object);
            Assert.That(res, Is.EqualTo(29000));
           
        }

        [TestCase]
        public void OrderSumTestMockShouldBeZero()
        {
            int res = stat.OrderSumCalc(new DateTime(1997, 1, 1).Date, new DateTime(1997, 1, 1).Date, mockctx.Object);
            Assert.That(res, Is.EqualTo(0));

        }
        [TestCase]
        public void OrderSumTestMockShouldBeTwo()
        {
            int res = stat.OrderSumCalc(new DateTime(1997, 1, 1).Date, new DateTime(2005, 1, 1).Date, mockctx.Object);
            Assert.That(res, Is.EqualTo(2));

        }

        [TestCase]
        public void AvgRatingTestMockShouldBezero()
        {
            double res = stat.AvgCalc(new DateTime(2014, 1, 1).Date, new DateTime(2016, 1, 1).Date, mockctx.Object);
            Assert.That(res, Is.EqualTo(0.0));
        }

        [TestCase]
        public void AvgRatingTestMockShouldBe3()
        {
            double res = stat.AvgCalc(new DateTime(199, 1, 1).Date, new DateTime(2005, 1, 1).Date, mockctx.Object);
            Assert.That(res, Is.EqualTo(3.0));
        }


        private Mock<PosDatabaseEntities> CreateMockObjectForIncomeSum()
        {
            Mock<PosDatabaseEntities> pos = new Mock<PosDatabaseEntities>();

            List<ORDER> orders = new List<ORDER>()
            {
                new ORDER()
                {
                    ORDERID = 1,
                    ORDERDATE = new DateTime(2004,1,1),
                    RATING = 5,
                    TOTALPRICE = 8000
                },
                new ORDER()
                {
                    ORDERID = 2,
                    ORDERDATE = new DateTime(2000,1,1),
                    RATING = 1,
                    TOTALPRICE = 8000
                },
                new ORDER()
                {
                    ORDERID = 3,
                    ORDERDATE = new DateTime(2009,1,1),
                    RATING = 5,
                    TOTALPRICE =6500
                },
                new ORDER()
                {
                    ORDERID = 5,
                    ORDERDATE = new DateTime(2010,1,1),
                    RATING = 5,
                    TOTALPRICE = 6500
                },
                new ORDER()
                {
                    ORDERID = 7,
                    ORDERDATE = new DateTime(2015,1,1),
                    RATING = 0,
                    TOTALPRICE = 6500
                },
                new ORDER()
                {
                    ORDERID = 9,
                    ORDERDATE = new DateTime(2015,1,1),
                    RATING = 0,
                    TOTALPRICE = 6500
                }
            };
            orders.ElementAt(0).ORDERLISTITEMs.Add(new ORDERLISTITEM()
            {
                AMOUNT = 8000,
                ORDERID = 1,

            });
            orders.ElementAt(1).ORDERLISTITEMs.Add(new ORDERLISTITEM()
            {
                AMOUNT = 8000,
                ORDERID = 2,

            });
            pos.Setup(x => x.ORDERS).ReturnsDbSet(orders.AsQueryable());
            return pos;


        }
    }
}
