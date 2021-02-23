using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Clubhouse.Services
{
    public abstract class ClubhouseAPIRequest<T> /*: APIRequest<T>*/
    {
        public string Path;
        public HttpMethod Method;
        public Dictionary<string, string> QueryParameters;
        public object Content;
        //public File fileToUpload;
        public Uri contentUriToUpload;
        public String fileFieldName, fileMimeType;

        //private ProgressDialog progress;

        public bool RequiresAuthorization { get; protected set; } = true;
        public bool RequiresInitialization { get; protected set; } = true;

        readonly bool canceled;
        //Call currentRequest;

        public ClubhouseAPIRequest(HttpMethod method, String path)
        {
            Path = path;
            Method = method;
        }

        //      @Override
        //  public void cancel()
        //      {
        //          canceled = true;
        //          if (currentRequest != null)
        //              currentRequest.cancel();
        //      }

        //      @Override
        //  public APIRequest<T> exec()
        //      {
        //          ClubhouseAPIController.getInstance().execRequest(this);
        //          if (progress != null)
        //              progress.show();
        //          return this;
        //      }

        //      public ClubhouseAPIRequest<T> upload(String fieldName, String mimeType, File file)
        //      {
        //          fileFieldName = fieldName;
        //          fileToUpload = file;
        //          fileMimeType = mimeType;
        //          return this;
        //      }

        //      public ClubhouseAPIRequest<T> upload(String fieldName, Uri uri)
        //      {
        //          fileFieldName = fieldName;
        //          contentUriToUpload = uri;
        //          return this;
        //      }

        //      public ClubhouseAPIRequest<T> wrapProgress(Context context)
        //      {
        //          progress = new ProgressDialog(context);
        //          progress.setMessage(context.getString(R.string.loading));
        //          progress.setCancelable(false);
        //          return this;
        //      }

        //      private void dismissProgressDialog()
        //      {
        //          progress.dismiss();
        //          progress = null;
        //      }

        //      public void prepare() throws Exception
        //      {

        //      }

        //      public T parse(String resp) throws Exception
        //      {
        //return ClubhouseAPIController.getInstance().getGson().fromJson(resp, responseClass);
        //  }

        //  void onSuccess(T result)
        //  {
        //      if (progress != null)
        //          uiThreadHandler.post(this::dismissProgressDialog);
        //      invokeSuccessCallback(result);
        //  }

        //  void onError(ErrorResponse result)
        //  {
        //      if (progress != null)
        //          uiThreadHandler.post(this::dismissProgressDialog);
        //      invokeErrorCallback(result);
        //  }
    }
}
