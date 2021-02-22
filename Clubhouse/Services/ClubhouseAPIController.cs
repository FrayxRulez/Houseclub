using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clubhouse.Services
{
    public class ClubhouseAPIController
    {
        private static readonly ClubhouseAPIController instance;
        private const String TAG = "ClubhouseAPI";
        private const bool DEBUG = false;

        private const String API_URL = "https://www.clubhouseapi.com/api";
        //	private static final Uri API_URL=Uri.parse("http://192.168.0.51:8080/");
        private const String API_BUILD_ID = "304";
        private const String API_BUILD_VERSION = "0.1.28";
        private const String API_UA = "clubhouse/" + API_BUILD_ID + " (iPhone; iOS 13.5.1; Scale/3.00)";

        public const String PUBNUB_PUB_KEY = "pub-c-6878d382-5ae6-4494-9099-f930f938868b";
        public const String PUBNUB_SUB_KEY = "sub-c-a4abea84-9ca3-11ea-8e71-f2b83ac9263d";

        public const String TWITTER_ID = "NyJhARWVYU1X3qJZtC2154xSI";
        public const String TWITTER_SECRET = "ylFImLBFaOE362uwr4jut8S8gXGWh93S1TUKbkfh7jDIPse02o";

        public const String AGORA_KEY = "938de3e8055e42b281bb8c6f69c21f78";
        public const String SENTRY_KEY = "5374a416cd2d4009a781b49d1bd9ef44@o325556.ingest.sentry.io/5245095";
        public const String INSTABUG_KEY = "4e53155da9b00728caa5249f2e35d6b3";
        public const String AMPLITUDE_KEY = "9098a21a950e7cb0933fb5b30affe5be";

        //private WorkerThread apiThread;
        //private Gson gson = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSSZ").disableHtmlEscaping().setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES).create();
        private readonly HttpClient httpClient = new HttpClient();

        public ClubhouseAPIController()
        {
            //httpClient.DefaultRequestHeaders.Add("CH-Languages", locales.toLanguageTags());
            //httpClient.DefaultRequestHeaders.Add("CH-Locale", locales.get(0).toLanguageTag().replace('-', '_'));
            httpClient.DefaultRequestHeaders.Add("CH-Languages", "en_US");
            httpClient.DefaultRequestHeaders.Add("CH-Locale", "en_US");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("CH-AppBuild", API_BUILD_ID);
            httpClient.DefaultRequestHeaders.Add("CH-AppVersion", API_BUILD_VERSION);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", API_UA);
            httpClient.DefaultRequestHeaders.Add("CH-DeviceId", ClubhouseSession.deviceID);

        }

        public async void Send<T>(ClubhouseAPIRequest<T> request)
        {
            await SendAsync(request);
        }

        public async Task<T> SendAsync<T>(ClubhouseAPIRequest<T> request)
        {
            try
            {
                //if (request.canceled)
                //    return;
                //Uri.Builder uri = API_URL.buildUpon().appendPath(req.path);
                var url = $"{API_URL}/{request.path}";

                if (request.queryParams != null)
                {
                    var index = 0;

                    foreach (var entry in request.queryParams)
                    {
                        if (index > 0)
                        {
                            url += "&";
                        }
                        else
                        {
                            url += "?";
                        }

                        url += $"{entry.Key}={WebUtility.UrlEncode(entry.Value)}";
                        index++;
                    }
                }

                //req.prepare();
                HttpContent reqBody = null;
                if (request.requestBody != null)
                {
                    //reqBody = RequestBody.create(MediaType.get("application/json; charset=utf-8"), gson.toJson(req.requestBody));
                    reqBody = new StringContent(JsonSerializer.Serialize(request.requestBody), Encoding.UTF8, "application/json");
                }
                //else if (req.fileToUpload != null && req.fileFieldName != null)
                //{
                //    reqBody = new MultipartBody.Builder()
                //            .setType(MultipartBody.FORM)
                //            .addFormDataPart(req.fileFieldName, req.fileToUpload.getName(), RequestBody.create(MediaType.get(req.fileMimeType), req.fileToUpload))
                //            .build();
                //}
                //else if (req.contentUriToUpload != null && req.fileFieldName != null)
                //{
                //    ContentUriRequestBody part = new ContentUriRequestBody(req.contentUriToUpload);
                //    reqBody = new MultipartBody.Builder()
                //            .setType(MultipartBody.FORM)
                //            .addFormDataPart(req.fileFieldName, part.getFileName(), part)
                //            .build();
                //}
                //if (DEBUG)
                //    Log.i(TAG, "Sending: " + req.method + " " + uri);
                //Request.Builder bldr = new Request.Builder()
                //        .url(uri.build().toString());
                var httpRequest = new HttpRequestMessage(request.method, url);
                httpRequest.Content = reqBody;

                //if ("POST".equals(req.method) && reqBody == null)
                //{
                //    Field fld = bldr.getClass().getDeclaredField("method");
                //    fld.setAccessible(true);
                //    fld.set(bldr, req.method);
                //}
                //else
                //{
                //    bldr.method(req.method, reqBody);
                //}
                //LocaleList locales = App.applicationContext.getResources().getConfiguration().getLocales();
                //bldr.header("CH-Languages", locales.toLanguageTags())
                //        .header("CH-Locale", locales.get(0).toLanguageTag().replace('-', '_'))
                //        .header("Accept", "application/json")
                //        .header("CH-AppBuild", API_BUILD_ID)
                //        .header("CH-AppVersion", API_BUILD_VERSION)
                //        .header("User-Agent", API_UA)
                //        .header("CH-DeviceId", ClubhouseSession.deviceID);

                if (ClubhouseSession.isLoggedIn())
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", "Token " + ClubhouseSession.userToken);
                    httpRequest.Headers.Add("CH-UserID", $"{ClubhouseSession.userID}");
                }
                //Call call = httpClient.newCall(bldr.build());
                //if (DEBUG)
                //    Log.i(TAG, call.request().headers().toString());
                //req.currentRequest = call;
                var response = await httpClient.SendAsync(httpRequest);
                var body = response.Content;
                //if (DEBUG)
                //    Log.i(TAG, "Code: " + resp.code());
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    String respStr = await body.ReadAsStringAsync();
                    //if (DEBUG)
                    //    Log.i(TAG, "Raw response: " + respStr);
                    //						T robj=gson.fromJson(body.charStream(), req.responseClass);
                    T robj = JsonSerializer.Deserialize<T>(respStr); //req.parse(respStr);
                                                                     //if (DEBUG)
                                                                     //    Log.i(TAG, "Parsed response: " + robj);
                                                                     //req.onSuccess(robj);

                    return robj;
                }
                else
                {
                    //                  String respStr = body.string();
                    //                  if (DEBUG)
                    //                      Log.i(TAG, "Raw response: " + respStr);
                    //                  BaseResponse br = gson.fromJson(respStr, BaseResponse.class);
                    //req.onError(new ClubhouseErrorResponse(br));
                }
            }

            catch (Exception x)
            {
                //Log.w(TAG, x);
                //String msg = BuildConfig.DEBUG ? x.getLocalizedMessage() : null;
                //req.onError(new ClubhouseErrorResponse(-1, msg));
            }

            return default;
        }
    }
}
