using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SecProject.Models
{
    public class EnumerableDropDownViewModel
    {
        public IEnumerable<SelectListItem> Items { get; set; }
        public string SelectedItem { get; set; }

        public void Initialize()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem))
            {
                return;
            }
            foreach (var selectListItem in Items.Where(selectListItem => SelectedItem == selectListItem.Value))
            {
                selectListItem.Selected = true;
            }
        }

        public EnumerableDropDownViewModel(Dictionary<string, string> dict, string selectedItem)
        {
            Items = dict.Select(item => new SelectListItem { Selected = selectedItem == item.Value, Text = item.Value.Replace('_',' '), Value = item.Key.ToString(CultureInfo.InvariantCulture) });
            SelectedItem = String.IsNullOrEmpty(selectedItem) ? null : selectedItem;
            Initialize();
        }

        public EnumerableDropDownViewModel() { }
    }
}