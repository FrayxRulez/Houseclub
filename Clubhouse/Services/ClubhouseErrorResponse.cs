namespace Clubhouse.Services
{
    public class ClubhouseErrorResponse /*: ErrorResponse*/
    {

        private readonly string message;

        public ClubhouseErrorResponse(int code, string message)
        {
            this.message = message;
        }

        public ClubhouseErrorResponse(BaseResponse br)
        {
            message = br.errorMessage;
        }

        //@Override
        //public void bindErrorView(View view)
        //{
        //    TextView txt = view.findViewById(R.id.error_text);
        //    if (BuildConfig.DEBUG)
        //        txt.setText(view.getContext().getString(R.string.error_loading) + ":\n" + message);
        //    else
        //        txt.setText(R.string.error_loading);
        //}

        //@Override
        //public void showToast(Context context)
        //{
        //    if (BuildConfig.DEBUG)
        //        Toast.makeText(context, message, Toast.LENGTH_LONG).show();
        //    else
        //        Toast.makeText(context, R.string.error_loading, Toast.LENGTH_SHORT).show();
        //}
    }
}
