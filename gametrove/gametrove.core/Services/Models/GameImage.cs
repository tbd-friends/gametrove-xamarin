using System;
using Gametrove.Core.Infrastructure;

namespace Gametrove.Core.Services.Models
{
    public class GameImage : BaseViewModel
    {
        public Guid Id { get; set; }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (_url != value)
                {
                    _url = value;

                    OnPropertyChanged();
                }
            }
        }


        private bool _isCoverArt;
        public bool IsCoverArt
        {
            get => _isCoverArt;
            set
            {
                if (_isCoverArt != value)
                {
                    _isCoverArt = value;

                    OnPropertyChanged();
                }
            }
        }
    }
}