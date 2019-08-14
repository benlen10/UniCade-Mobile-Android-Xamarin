﻿﻿using Android.Content;
  using Android.Graphics;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

namespace UniCadeAndroid.Backend
{

	public class ScaleImageViewGestureDetector : GestureDetector.SimpleOnGestureListener
	{
		private readonly ScaleImageView _mScaleImageView;
		public ScaleImageViewGestureDetector(ScaleImageView imageView)
		{
			_mScaleImageView = imageView;
		}

		public override bool OnDown(MotionEvent e)
		{
			return true;
		}

		public override bool OnDoubleTap(MotionEvent e)
		{
			_mScaleImageView.MaxZoomTo((int)e.GetX(), (int)e.GetY());
			_mScaleImageView.Cutting();
			return true;
		}
	}

	public class ScaleImageView : ImageView, View.IOnTouchListener
	{
		private readonly Context _mContext;

		private readonly float _mMaxScale = 8.0f;

		private Matrix _mMatrix;
		private readonly float[] _mMatrixValues = new float[9];
		private int _mWidth;
		private int _mHeight;
		private int _mIntrinsicWidth;
		private int _mIntrinsicHeight;
		private float _mScale;
		private float _mMinScale;
		private float _mPreviousDistance;
		private int _mPreviousMoveX;
		private int _mPreviousMoveY;

		private bool _mIsScaling;
		private GestureDetector _mGestureDetector;

		public ScaleImageView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			_mContext = context;
			Initialize();
		}

		public ScaleImageView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			_mContext = context;
			Initialize();
		}

		public override void SetImageBitmap(Bitmap bm)
		{
			base.SetImageBitmap(bm);
			Initialize();
		}

		public override void SetImageResource(int resId)
		{
			base.SetImageResource(resId);
			Initialize();
		}

		private void Initialize()
		{
			SetScaleType(ScaleType.Matrix);
			_mMatrix = new Matrix();

			if (Drawable != null)
			{
				_mIntrinsicWidth = Drawable.IntrinsicWidth;
				_mIntrinsicHeight = Drawable.IntrinsicHeight;
				SetOnTouchListener(this);
			}

			_mGestureDetector = new GestureDetector(_mContext, new ScaleImageViewGestureDetector(this));
		}

		protected override bool SetFrame(int l, int t, int r, int b)
		{
			_mWidth = r - l;
			_mHeight = b - t;

			_mMatrix.Reset();
			var rNorm = r - l;
			_mScale = rNorm / (float)_mIntrinsicWidth;

			var paddingHeight = 0;
			var paddingWidth = 0;
			if (_mScale * _mIntrinsicHeight > _mHeight)
			{
				_mScale = _mHeight / (float)_mIntrinsicHeight;
				_mMatrix.PostScale(_mScale, _mScale);
				paddingWidth = (r - _mWidth) / 2;
			}
			else
			{
				_mMatrix.PostScale(_mScale, _mScale);
				paddingHeight = (b - _mHeight) / 2;
			}

			_mMatrix.PostTranslate(paddingWidth, paddingHeight);
			ImageMatrix = _mMatrix;
			_mMinScale = _mScale;
			ZoomTo(_mScale, _mWidth / 2, _mHeight / 2);
			Cutting();
			return base.SetFrame(l, t, r, b);
		}

		private float GetValue(Matrix matrix, int whichValue)
		{
			matrix.GetValues(_mMatrixValues);
			return _mMatrixValues[whichValue];
		}



		public float Scale => GetValue(_mMatrix, Matrix.MscaleX);

	    public float TranslateX => GetValue(_mMatrix, Matrix.MtransX);

	    public float TranslateY => GetValue(_mMatrix, Matrix.MtransY);

	    public void MaxZoomTo(int x, int y)
		{
			if (_mMinScale != Scale && (Scale - _mMinScale) > 0.1f)
			{
				var scale = _mMinScale / Scale;
				ZoomTo(scale, x, y);
			}
			else
			{
				var scale = _mMaxScale / Scale;
				ZoomTo(scale, x, y);
			}
		}

		public void ZoomTo(float scale, int x, int y)
		{
			if (Scale * scale < _mMinScale)
			{
				scale = _mMinScale / Scale;
			}
			else
			{
				if (scale >= 1 && Scale * scale > _mMaxScale)
				{
					scale = _mMaxScale / Scale;
				}
			}
			_mMatrix.PostScale(scale, scale);
			//move to center
			_mMatrix.PostTranslate(-(_mWidth * scale - _mWidth) / 2, -(_mHeight * scale - _mHeight) / 2);

			//move x and y distance
			_mMatrix.PostTranslate(-(x - (_mWidth / 2)) * scale, 0);
			_mMatrix.PostTranslate(0, -(y - (_mHeight / 2)) * scale);
			ImageMatrix = _mMatrix;
		}

		public void Cutting()
		{
			var width = (int)(_mIntrinsicWidth * Scale);
			var height = (int)(_mIntrinsicHeight * Scale);
			if (TranslateX < -(width - _mWidth))
			{
				_mMatrix.PostTranslate(-(TranslateX + width - _mWidth), 0);
			}

			if (TranslateX > 0)
			{
				_mMatrix.PostTranslate(-TranslateX, 0);
			}

			if (TranslateY < -(height - _mHeight))
			{
				_mMatrix.PostTranslate(0, -(TranslateY + height - _mHeight));
			}

			if (TranslateY > 0)
			{
				_mMatrix.PostTranslate(0, -TranslateY);
			}

			if (width < _mWidth)
			{
				_mMatrix.PostTranslate((_mWidth - width) / 2, 0);
			}

			if (height < _mHeight)
			{
				_mMatrix.PostTranslate(0, (_mHeight - height) / 2);
			}

			ImageMatrix = _mMatrix;
		}

		private float Distance(float x0, float x1, float y0, float y1)
		{
			var x = x0 - x1;
			var y = y0 - y1;
			return FloatMath.Sqrt(x * x + y * y);
		}

		private float DispDistance()
		{
			return FloatMath.Sqrt(_mWidth * _mWidth + _mHeight * _mHeight);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			if (_mGestureDetector.OnTouchEvent(e))
			{
				_mPreviousMoveX = (int)e.GetX();
				_mPreviousMoveY = (int)e.GetY();
				return true;
			}

			var touchCount = e.PointerCount;
			switch (e.Action)
			{
				case MotionEventActions.Down:
				case MotionEventActions.Pointer1Down:
				case MotionEventActions.Pointer2Down:
					{
						if (touchCount >= 2)
						{
							var distance = Distance(e.GetX(0), e.GetX(1), e.GetY(0), e.GetY(1));
							_mPreviousDistance = distance;
							_mIsScaling = true;
						}
					}
					break;

				case MotionEventActions.Move:
					{
						if (touchCount >= 2 && _mIsScaling)
						{
							var distance = Distance(e.GetX(0), e.GetX(1), e.GetY(0), e.GetY(1));
							var scale = (distance - _mPreviousDistance) / DispDistance();
							_mPreviousDistance = distance;
							scale += 1;
							scale = scale * scale;
							ZoomTo(scale, _mWidth / 2, _mHeight / 2);
							Cutting();
						}
						else if (!_mIsScaling)
						{
							var distanceX = _mPreviousMoveX - (int)e.GetX();
							var distanceY = _mPreviousMoveY - (int)e.GetY();
							_mPreviousMoveX = (int)e.GetX();
							_mPreviousMoveY = (int)e.GetY();

							_mMatrix.PostTranslate(-distanceX, -distanceY);
							Cutting();
						}
					}
					break;
				case MotionEventActions.Up:
				case MotionEventActions.Pointer1Up:
				case MotionEventActions.Pointer2Up:
					{
						if (touchCount <= 1)
						{
							_mIsScaling = false;
						}
					}
					break;
			}
			return true;
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			return OnTouchEvent(e);
		}
	}
}