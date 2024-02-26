using System.ComponentModel.DataAnnotations;

namespace FinalFilmProject.DTOS
{
    public class CinemaRoomDistanceFilterDTO
    {     
        [Range(-90,90)]
        public double Latitude { get; set; }      
        [Range(-180,180)]
        public double Longitude { get; set; }

        private int distanceInKms = 10;
        private int maxDistanceInKms = 50;
        public int DistanceInKms
        {
            get => distanceInKms;
            set => distanceInKms = (value > maxDistanceInKms) ? distanceInKms : value;
        }     
    }
}