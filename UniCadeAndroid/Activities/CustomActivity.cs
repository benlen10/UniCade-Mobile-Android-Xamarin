using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UniCadeAndroid.Objects;

namespace UniCadeAndroid.Activities
{
    public class CustomActivity : BaseAdapter<CustomModel>
    {

        List<CustomModel> items;

        Activity context;

        public CustomActivity(Activity context, List<CustomModel> items) : base()

        {


            this.context = context;

            this.items = items;


        }

        public override CustomModel this[int position]

        {

            get { return items[position]; }

        }

        public override int Count

        {

            get { return items.Count; }

        }

        public override long GetItemId(int position)

        {

            return position;

        }

        public override View GetView(int position, View convertView, ViewGroup parent)

        {

            var item = items[position];

            View view = convertView;

            if (view == null) // no view to re-use, create new

                view = context.LayoutInflater.Inflate(Resource.Layout.CustomList, null);

            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Title;

            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Author;

            view.FindViewById<TextView>(Resource.Id.Text3).Text = item.Description;

            return view;

        }
    }
}