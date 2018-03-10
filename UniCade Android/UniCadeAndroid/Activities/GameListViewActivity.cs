using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using UniCadeAndroid.Objects;

namespace UniCadeAndroid.Activities
{
    public class GameListViewActivity : BaseAdapter<GameListObject>
    {
        readonly List<GameListObject> _items;
        readonly Activity _context;
        public GameListViewActivity(Activity context, List<GameListObject> items)
        {
            _context = context;
            _items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override GameListObject this[int position] => _items[position];

        public override int Count => _items.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            View view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.CustomGameListView, null);

            // no view to re-use, create new
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Title;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Console;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageResourceId);
            return view;
        }
    }
}