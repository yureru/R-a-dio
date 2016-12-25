﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Radio
{
    /// <summary>
    /// Lógica de interacción para SongListTwo.xaml
    /// </summary>
    public partial class SongList : Window
    {

        public string item = "Hello";

        public SongFromList[] items = { new SongFromList() { Name = "ChouCho - Authentic Symphony", ID = 1, IsFavorite = true},
            new SongFromList() { Name = "GRANRODEO - Summer GT09", ID = 2, IsFavorite = false},
            new SongFromList() { Name = "Rica Matsumoto - Alive A life", ID = 3, IsFavorite = true},
            new SongFromList() { Name = "Yoko Kanno - Arcadia", ID = 4, IsFavorite = false},
            new SongFromList() { Name = "in feat. IA - Ayano's Theory of Happiness", ID = 5, IsFavorite = true},
        };
        public SongList()
        {
            InitializeComponent();
            //newCont.ItemsSource = items;
            //newCont.ItemsSource = Database.GetRangeOfRecords(Page.CurrentNumber, Updater.DBConnection).Result;

            // Going to make CreatePagination public to execute in a task
            CreatePagination();

            //CreatePaginationAsync();

            /*favCont.ItemsSource = items;
            delCont.ItemsSource = items;*/
            /*SongContainer.DataContext = items;
            SongContainer.ItemsSource = items;*/
        }

        public async void CreatePagination()
        {
            /*int numberOfPages = await Database.NumberOfPages(Updater.DBConnection);
            PaginationNumber.Text = numberOfPages.ToString();*/
            // current
            /*var pagesqt = Database.NumberOfPages(Updater.DBConnection).Result;
            var items = new List<Page>();
            for (int i = 0; i < pagesqt; ++i)
            {
                // TODO: I don't like passing newCont to every new object, it's dumb since that is saved in a static member.
                items.Add(new Page(newCont) { Number = i + 1, IsSelected = (i == 0) ? true : false });
            }
            PaginationNumber.ItemsSource = items;*/
            PaginationNumber.ItemsSource = Page.GetNewButtonList(currPageList);

        }

        /*public async void CreatePaginationAsync()
        {
            Task t = Task.Run(() =>
            {
                PaginationNumber.ItemsSource = Page.GetNewButtonList(currPageList);
            });

            await t;
        }*/

        public async void CreatePaginationAsync()
        {
            //PaginationNumber.ItemsSource = Page.GetNewButtonList(currPageList);
            Page._controlList = currPageList;
            PaginationNumber.ItemsSource = Page.GetNewButtonList();
            currPageList.ItemsSource = SongFromList.CurrentList;
        }

    }
}
