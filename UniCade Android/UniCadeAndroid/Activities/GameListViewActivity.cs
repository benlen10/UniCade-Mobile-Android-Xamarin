using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using UniCadeAndroid.Objects;

namespace UniCadeAndroid.Activities
{
    public class GameListViewActivity : BaseAdapter<GameListObject>
    {
        List<GameListObject> items;
        Activity context;
        public GameListViewActivity(Activity context, List<GameListObject> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override GameListObject this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            // no view to re-use, create new
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomGameListView, null);
            }
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Title;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Console;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageResourceId);
            return view;
        }
    }
}