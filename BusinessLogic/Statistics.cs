using DataLayer;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogic
{
    public class Statistics : Bindable
    {
        private int incomeSum;
        private int orderSum;
        private PRODUCT maxProduct;
        private PRODUCT minProduct;
        private double avgRating;
        private SeriesCollection chart;

        /// <summary>
        /// Initializes a new instance of the <see cref="Statistics"/> class.
        /// </summary>
        public Statistics()
        {
            this.Chart = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int>(),
                    LabelPoint = point => point.Y + " db"
                }
            };

            this.Labels = new List<string>();
        }

        public Statistics(int i)
        {

        }

        /// <summary>
        /// Gets or sets the starting date of the stats.
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the ending date of the stats.
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Gets or sets the most popular product.
        /// </summary>
        public PRODUCT MaxProduct { get => this.maxProduct; set => this.SetProperty(ref this.maxProduct, value); }

        /// <summary>
        /// Gets or sets the least popular product.
        /// </summary>
        public PRODUCT MinProduct { get => this.minProduct; set => this.SetProperty(ref this.minProduct, value); }

        /// <summary>
        /// Gets or sets the average of the ratings.
        /// </summary>
        public double AvgRating { get => this.avgRating; set => this.SetProperty(ref this.avgRating, value); }

        /// <summary>
        /// Gets or sets the sum of the order totals.
        /// </summary>
        public int IncomeSum { get => this.incomeSum; set => this.SetProperty(ref this.incomeSum, value); }

        /// <summary>
        /// Gets or sets the sum of the orders.
        /// </summary>
        public int OrderSum { get => this.orderSum; set => this.SetProperty(ref this.orderSum, value); }

        /// <summary>
        /// Gets or sets the series collection of a chart.
        /// </summary>
        public SeriesCollection Chart { get => this.chart; set => this.SetProperty(ref this.chart, value); }

        /// <summary>
        /// Gets or sets the chart labels
        /// </summary>
        public List<string> Labels { get; set; }

        /// <summary>
        /// Updates the statistics datas.
        /// </summary>
        /// <param name="from">The starting date of the statistics.</param>
        /// <param name="to">The ending date of the statistics</param>
        /// <param name="ctx">The database context.</param>
        public void Update(DateTime from, DateTime to, PosDatabaseEntities ctx)
        {
            this.DateFrom = from.Date;
            this.DateTo = to.Date;

            var orderExist = ctx.ORDERS.Where(x => (DbFunctions.TruncateTime(x.ORDERDATE) >= this.DateFrom && DbFunctions.TruncateTime(x.ORDERDATE) <= this.DateTo)).ToList().Count > 0;

            if (this.DateFrom <= this.DateTo && orderExist)
            {
                this.IncomeSum = (int)ctx.ORDERS.Where(x => (DbFunctions.TruncateTime(x.ORDERDATE) >= this.DateFrom && DbFunctions.TruncateTime(x.ORDERDATE) <= this.DateTo)).Sum(x => x.TOTALPRICE);

                this.OrderSum = ctx.ORDERS.Where(x => (DbFunctions.TruncateTime(x.ORDERDATE) >= this.DateFrom && DbFunctions.TruncateTime(x.ORDERDATE) <= this.DateTo)).Count();

                var groupByAmountSum = ctx.ORDERLISTITEMs.Where(x => (DbFunctions.TruncateTime(x.ORDER.ORDERDATE) >= this.DateFrom && DbFunctions.TruncateTime(x.ORDER.ORDERDATE) <= this.DateTo)).GroupBy(x => x.PRODUCT1).Select(x => new { Termek = x.Select(y => y.PRODUCT1).FirstOrDefault(), Amount = x.Sum(y => y.AMOUNT) }).ToList();

                var maxAmount = groupByAmountSum.Max(x => x.Amount);
                this.MaxProduct = groupByAmountSum.FirstOrDefault(x => x.Amount == maxAmount)?.Termek;

                var minAmount = groupByAmountSum.Min(x => x.Amount);
                this.MinProduct = groupByAmountSum.FirstOrDefault(x => x.Amount == minAmount)?.Termek;

                var q = ctx.ORDERS.Where(x => ((DbFunctions.TruncateTime(x.ORDERDATE) >= this.DateFrom && DbFunctions.TruncateTime(x.ORDERDATE) <= this.DateTo) && x.RATING > 0)).ToList();
                this.AvgRating = q.Count > 0 ? Math.Round((double)q.Average(x => x.RATING), 2) : 0;

                this.Chart[0].Values.Clear();
                this.Labels.Clear();

                int interval = (this.DateTo - this.DateFrom).Days;
                if (interval < 1)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        var date = this.DateFrom.AddHours(i);
                        var next = this.DateFrom.AddHours(i + 1);
                        this.Chart[0].Values.Add(ctx.ORDERS.Where(x => x.ORDERDATE >= date && x.ORDERDATE < next).Count());
                        this.Labels.Add(date.ToString("HH:mm"));
                    }
                }
                else
                {
                    for (int i = 0; i <= interval; i++)
                    {
                        var date = this.DateFrom.AddDays(i);
                        this.Chart[0].Values.Add(ctx.ORDERS.Where(x => (DbFunctions.TruncateTime(x.ORDERDATE) == date)).Count());
                        this.Labels.Add(date.ToString("MM.dd"));
                    }
                }
            }
            else
            {
                if (this.OrderSum != 0)
                {
                    MessageBox.Show("Nem létezik rendelés a kiválasztott időszakban!");
                }

                this.OrderSum = 0;
                this.IncomeSum = 0;
                this.MaxProduct = new PRODUCT { PNAME = "N/A" };
                this.MinProduct = new PRODUCT { PNAME = "N/A" };
                this.AvgRating = 0;
            }
        }

        public int CalcIncome(DateTime from, DateTime to, PosDatabaseEntities ctx)
        {
            return (int)ctx.ORDERS.Where(x => (TestableDbFunctions.TruncateTime(x.ORDERDATE) >= from.Date && TestableDbFunctions.TruncateTime(x.ORDERDATE) <= to.Date)).Sum(x => x.TOTALPRICE);
        }

        public int OrderSumCalc(DateTime from, DateTime to, PosDatabaseEntities ctx)
        {
            return (int)ctx.ORDERS.Where(x => (TestableDbFunctions.TruncateTime(x.ORDERDATE) >= from.Date && TestableDbFunctions.TruncateTime(x.ORDERDATE) <= to.Date)).Count();
        }

        public double AvgCalc(DateTime from, DateTime to, PosDatabaseEntities ctx)
        {
            var q = ctx.ORDERS.Where(x => ((TestableDbFunctions.TruncateTime(x.ORDERDATE) >= from.Date && TestableDbFunctions.TruncateTime(x.ORDERDATE) <= to.Date) && x.RATING > 0)).ToList();
            return q.Count > 0 ? Math.Round((double)q.Average(x => x.RATING), 2) : 0;
        }
    }
}

