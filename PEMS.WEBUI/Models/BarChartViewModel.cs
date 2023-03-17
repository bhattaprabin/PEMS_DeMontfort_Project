using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEMS.WEBUI.Models
{
    public class BarChartViewModel
    {
        public BarChartViewModel()
        {
            labels = new List<string>();
            datasets = new List<BarChartChildViewModel>();
            
        }
        public List<string> labels { get; set; }
        public List<BarChartChildViewModel> datasets { get; set; }

    }
    public class BarChartChildViewModel
    {
        public BarChartChildViewModel()
        {
            data = new List<int>();
            backgroundColor = new List<string>();
        }
        public string label { get; set; }
        public List<string> backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int borderWidth { get; set; }
        public string hoverBackgroundColor { get; set; }
        public string hoverBorderColor { get; set; }
        public List<int> data { get; set; }
    }
}