using Clubhouse.Models;
using Clubhouse.Services.Methods;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clubhouse.Services
{
    public interface IDataService
    {
        User Me { get; }

        ICollection<ulong> BlockedIds { get; }
        ICollection<ulong> FollowingIds { get; }

        Task InitializeAsync();

        void Send<T>(ClubhouseAPIRequest<T> request) /*where T : BaseResponse*/ ;
        Task<T> SendAsync<T>(ClubhouseAPIRequest<T> request) /*where T : BaseResponse*/ ;
    }

    public class ClubhouseAPIController : IDataService
    {
        private const string TAG = "ClubhouseAPI";
        private const bool DEBUG = false;

        private const string API_URL = "https://www.clubhouseapi.com/api";
        private const string API_BUILD_ID = "304";
        private const string API_BUILD_VERSION = "0.1.28";
        private const string API_UA = "clubhouse/" + API_BUILD_ID + " (iPhone; iOS 13.5.1; Scale/3.00)";

        public const string PUBNUB_PUB_KEY = "pub-c-6878d382-5ae6-4494-9099-f930f938868b";
        public const string PUBNUB_SUB_KEY = "sub-c-a4abea84-9ca3-11ea-8e71-f2b83ac9263d";

        public const string TWITTER_ID = "NyJhARWVYU1X3qJZtC2154xSI";
        public const string TWITTER_SECRET = "ylFImLBFaOE362uwr4jut8S8gXGWh93S1TUKbkfh7jDIPse02o";

        public const string AGORA_KEY = "938de3e8055e42b281bb8c6f69c21f78";
        public const string SENTRY_KEY = "5374a416cd2d4009a781b49d1bd9ef44@o325556.ingest.sentry.io/5245095";
        public const string INSTABUG_KEY = "4e53155da9b00728caa5249f2e35d6b3";
        public const string AMPLITUDE_KEY = "9098a21a950e7cb0933fb5b30affe5be";

        private readonly HashSet<ulong> _blockedIds = new HashSet<ulong>();
        private readonly HashSet<ulong> _followingIds = new HashSet<ulong>();

        //private WorkerThread apiThread;
        //private Gson gson = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSSZ").disableHtmlEscaping().setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES).create();
        private readonly HttpClient _client = new HttpClient();
        private readonly TaskCompletionSource<bool> _initializeTask = new TaskCompletionSource<bool>();

        public ClubhouseAPIController()
        {
            //httpClient.DefaultRequestHeaders.Add("CH-Languages", locales.toLanguageTags());
            //httpClient.DefaultRequestHeaders.Add("CH-Locale", locales.get(0).toLanguageTag().replace('-', '_'));
            _client.DefaultRequestHeaders.Add("CH-Languages", "en_US");
            _client.DefaultRequestHeaders.Add("CH-Locale", "en_US");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("CH-AppBuild", API_BUILD_ID);
            _client.DefaultRequestHeaders.Add("CH-AppVersion", API_BUILD_VERSION);
            _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", API_UA);
            _client.DefaultRequestHeaders.Add("CH-DeviceId", ClubhouseSession.deviceID);

            Initialize();
        }

        public User Me { get; private set; }

        public ICollection<ulong> BlockedIds => _blockedIds;
        public ICollection<ulong> FollowingIds => _followingIds;

        private async void Initialize()
        {
            if (ClubhouseSession.isLoggedIn())
            {
                var response = await SendAsync(new Me(true, true));
                if (response.Success)
                {
                    Me = response.UserProfile;

                    _blockedIds.Clear();
                    _followingIds.Clear();

                    _blockedIds.UnionWith(response.BlockedIds);
                    _followingIds.UnionWith(response.FollowingIds);
                }

                _initializeTask.SetResult(true);
            }
        }

        public Task InitializeAsync()
        {
            return _initializeTask.Task;
        }

        public async void Send<T>(ClubhouseAPIRequest<T> request) /*where T : BaseResponse*/
        {
            await SendAsync(request);
        }

        public async Task<T> SendAsync<T>(ClubhouseAPIRequest<T> request) /*where T : BaseResponse*/
        {
            var response = await SendAsyncInternal<T>(request);

            if (response is BaseResponse resp && resp.Success)
            {
                if (request is Follow follow)
                {
                    FollowingIds.Add(follow.UserId);
                }
                else if (request is Unfollow unfollow)
                {
                    FollowingIds.Remove(unfollow.UserId);
                }
            }

            return response;
        }

        public async Task<T> SendAsyncInternal<T>(ClubhouseAPIRequest<T> request) /*where T : BaseResponse*/
        {
            try
            {
                if (request.RequiresInitialization)
                {
                    await _initializeTask.Task;
                }

                //if (request.canceled)
                //    return;
                //Uri.Builder uri = API_URL.buildUpon().appendPath(req.path);
                var url = $"{API_URL}/{request.Path}";

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
                if (request.Content != null)
                {
                    //reqBody = RequestBody.create(MediaType.get("application/json; charset=utf-8"), gson.toJson(req.requestBody));
                    reqBody = new StringContent(JsonSerializer.Serialize(request.Content), Encoding.UTF8, "application/json");
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
                var httpRequest = new HttpRequestMessage(request.Method, url);
                httpRequest.Content = reqBody;

                if (ClubhouseSession.isLoggedIn())
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", "Token " + ClubhouseSession.userToken);
                    httpRequest.Headers.Add("CH-UserID", $"{ClubhouseSession.userID}");
                }
                //Call call = httpClient.newCall(bldr.build());
                //if (DEBUG)
                //    Log.i(TAG, call.request().headers().toString());
                //req.currentRequest = call;
                var response = await _client.SendAsync(httpRequest);
                var body = response.Content;
                //if (DEBUG)
                //    Log.i(TAG, "Code: " + resp.code());
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string respStr = await body.ReadAsStringAsync();
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
                    string respStr = await body.ReadAsStringAsync();
                    //                  String respStr = body.string();
                    //                  if (DEBUG)
                    //                      Log.i(TAG, "Raw response: " + respStr);
                    //                  BaseResponse br = gson.fromJson(respStr, BaseResponse.class);
                    //req.onError(new ClubhouseErrorResponse(br));
                    T robj = JsonSerializer.Deserialize<T>(respStr); //req.parse(respStr);
                                                                     //if (DEBUG)
                                                                     //    Log.i(TAG, "Parsed response: " + robj);
                                                                     //req.onSuccess(robj);

                    return robj;
                }
            }

            catch (Exception x)
            {
                //Log.w(TAG, x);
                //String msg = BuildConfig.DEBUG ? x.getLocalizedMessage() : null;
                //req.onError(new ClubhouseErrorResponse(-1, msg));
                T robj = Activator.CreateInstance<T>();

                if (robj is BaseResponse resp)
                {
                    resp.Success = false;
                    resp.ErrorMessage = "Internal error";

                    return robj;
                }

                return default;
            }
        }
    }
}
