using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZOOM_SDK_DOTNET_WRAP;

namespace ZoomSDKDemoForWebinar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //register callback
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onAuthenticationReturn(onAuthenticationReturn);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLoginRet(onLoginRet);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLogout(onLogout);
            ZOOM_SDK_DOTNET_WRAP.AuthParam param = new ZOOM_SDK_DOTNET_WRAP.AuthParam();
            //

            //param.appKey = "4AIahTCShwgx9wvhf65m3SC6Dh5S6dIxeWOq";
            //param.appSecret = "HrDjWFQfreo3UBeg9FVyfJSPKDgJXmDvnZ6b";
            //param.appKey = "ZbfGVCW3FBM0IULTkp9Xn1h6QBfWSxUCHZ1Z";
            //param.appSecret = "g2s1G64OFqenitYg2kzp5PPINjGS7uv2gqNU";
            param.appKey = "laXgngPcCX8vXilceX4HiqCocj3sETsleGtr";
            param.appSecret = "UNxw4mFtC1UpTIXg1CaYksDPeivTPsVnAGsE";
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().SDKAuth(param);
            Hide();
        }

        //callback
        public void onAuthenticationReturn(AuthResult ret)
        {
            if (ZOOM_SDK_DOTNET_WRAP.AuthResult.AUTHRET_SUCCESS == ret)
            {
                Show();
            }
            else//error handle.todo
            {
                //Show();
            }
        }
        public void onLoginRet(LOGINSTATUS ret, IAccountInfo pAccountInfo)
        {
            //todo
        }
        public void onLogout()
        {
            //todo
        }

        //ZOOM_SDK_DOTNET_WRAP.onMeetingStatusChanged
        public void onMeetingStatusChanged(MeetingStatus status, int iResult)
        {
            switch (status)
            {
                case ZOOM_SDK_DOTNET_WRAP.MeetingStatus.MEETING_STATUS_ENDED:
                case ZOOM_SDK_DOTNET_WRAP.MeetingStatus.MEETING_STATUS_FAILED:
                    {
                        Show();
                    }
                    break;
                default://todo
                    break;
            }
        }

        public void onUserJoin(Array lstUserID)
        {
            if (null == (Object)lstUserID)
                return;

            for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
            {
                UInt32 userid = (UInt32)lstUserID.GetValue(i);
                ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                    GetMeetingParticipantsController().GetUserByUserID(userid);
                if (null != (Object)user)
                {
                    string name = user.GetUserNameW();
                    Console.Write(name);
                }
            }
        }
        public void onUserLeft(Array lstUserID)
        {
            //todo
        }
        public void onHostChangeNotification(UInt32 userId)
        {
            //todo
        }
        public void onLowOrRaiseHandStatusChanged(bool bLow, UInt32 userid)
        {
            //todo
        }
        public void onUserNameChanged(UInt32 userId, string userName)
        {
            //todo
        }
        public void onRecording2MP4Done(bool bsuccess, int iResult, string szPath)
        {
            if (bsuccess)
            {
                String path = szPath;
            }
        }

        private void RegisterCallBack()
        {
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Add_CB_onMeetingStatusChanged(onMeetingStatusChanged);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onHostChangeNotification(onHostChangeNotification);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onLowOrRaiseHandStatusChanged(onLowOrRaiseHandStatusChanged);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserJoin(onUserJoin);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserLeft(onUserLeft);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserNameChanged(onUserNameChanged);

            //ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingConfiguration().EnableInviteButtonOnMeetingUI(true);
            //ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().Add_CB_onInviteBtnClicked(onInviteBtnClicked);

            //ZOOM_SDK_DOTNET_WRAP.CMeetingRecordingControllerDotNetWrap.Instance.Add_CB_onRecording2MP4Done(onRecording2MP4Done);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingRecordingController().Add_CB_onRecording2MP4Done(onRecording2MP4Done);

        }
        private void button_start_api_Click(object sender, RoutedEventArgs e)
        {
            RegisterCallBack();
            ZOOM_SDK_DOTNET_WRAP.StartParam param = new ZOOM_SDK_DOTNET_WRAP.StartParam();
            param.userType = ZOOM_SDK_DOTNET_WRAP.SDKUserType.SDK_UT_APIUSER;
            ZOOM_SDK_DOTNET_WRAP.StartParam4APIUser start_api_param = new ZOOM_SDK_DOTNET_WRAP.StartParam4APIUser();
            start_api_param.meetingNumber = UInt64.Parse(textBox_meetingnumber_api.Text);
            start_api_param.userID = textBox_userid_api.Text;
            start_api_param.userToken = textBox_usertoken_api.Text;
            start_api_param.userName = textBox_username_api.Text;
            param.apiuserStart = start_api_param;

            ZOOM_SDK_DOTNET_WRAP.SDKError err = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Start(param);
            if (ZOOM_SDK_DOTNET_WRAP.SDKError.SDKERR_SUCCESS == err)
            {
                Hide();
            }
            else//error handle
            { }

        }

        private void button_join_api_Click(object sender, RoutedEventArgs e)
        {
            RegisterCallBack();
            ZOOM_SDK_DOTNET_WRAP.JoinParam param = new ZOOM_SDK_DOTNET_WRAP.JoinParam();
            param.userType = ZOOM_SDK_DOTNET_WRAP.SDKUserType.SDK_UT_APIUSER;
            ZOOM_SDK_DOTNET_WRAP.JoinParam4APIUser join_api_param = new ZOOM_SDK_DOTNET_WRAP.JoinParam4APIUser();
            join_api_param.meetingNumber = UInt64.Parse(textBox_meetingnumber_api.Text);
            join_api_param.userName = textBox_username_api.Text;
            param.apiuserJoin = join_api_param;

            ZOOM_SDK_DOTNET_WRAP.SDKError err = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Join(param);
            if (ZOOM_SDK_DOTNET_WRAP.SDKError.SDKERR_SUCCESS == err)
            {
                Hide();
            }
            else//error handle
            { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_RedirShareFromeHome_Click(object sender, RoutedEventArgs e)
        {
            ShareFromHome ObjShareFromHome = new ShareFromHome();
            ObjShareFromHome.ShowDialog();
        }
    }
}
