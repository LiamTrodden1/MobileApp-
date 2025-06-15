using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumApp.Models;
using AlbumApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AlbumApp.ViewModels
{
    public partial class AlbumManagementViewModel : ObservableObject
    {
        private readonly AlbumDatabaseService _albumDatabaseService;

        public AlbumManagementViewModel()
        {
            _albumDatabaseService = new AlbumDatabaseService();
            Albums = new ObservableCollection<AlbumManagementModel>();
            LoadAlbums();
        }

        //auto update all stored albums
        [ObservableProperty]
        ObservableCollection<AlbumManagementModel> albums;

        //load albums
        public async Task LoadAlbums()
        {
            //get all items from database
            var items = await _albumDatabaseService.GetItemsAsync();

            //clear existing albums
            Albums.Clear();
            //add all stored albums to show them
            foreach (var item in items)
            {
                Albums.Add(item);
            }
        }

        //if decrease button is used
        [RelayCommand]
        public async Task DecreaseCount(AlbumManagementModel album)
        {
            if (album.count > 0)
            {
                //decrease the count
                album.count--;
                await _albumDatabaseService.UpdateItemAsync(album);

                //delete from database and UI if count is 0
                if (album.count == 0)
                {
                    await _albumDatabaseService.DeleteItemAsync(album);
                    Albums.Remove(album);
                }
            }
            //reload all albums
            await LoadAlbums();
        }
    }
}
