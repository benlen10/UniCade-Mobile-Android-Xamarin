using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace UniCadeAndroid.Objects
{
    public class MobyGenre
    {
        public string Genre_category { get; set; }
        public int Genre_category_id { get; set; }
        public int Genre_id { get; set; }
        public string Genre_name { get; set; }
    }

    public class MobyPlatform
    {
        public string First_release_date { get; set; }
        public int Platform_id { get; set; }
        public string Platform_name { get; set; }
    }

    public class MobySampleCover
    {
        public int Height { get; set; }
        public string Image { get; set; }
        public List<string> Platforms { get; set; }
        public string Thumbnail_image { get; set; }
        public int Width { get; set; }
    }

    public class MobyGameResult
    {
        public string Description { get; set; }
        public int Game_id { get; set; }
        public List<MobyGenre> Genres { get; set; }
        public double Moby_score { get; set; }
        public string Moby_url { get; set; }
        public int Num_votes { get; set; }
        public object Official_url { get; set; }
        public List<MobyPlatform> Platforms { get; set; }
        public MobySampleCover MobySampleCover { get; set; }
        public List<object> Sample_screenshots { get; set; }
        public string Title { get; set; }
    }

    public class MobyRootObject
    {
        public List<MobyGameResult> Games { get; set; }
    }
}
