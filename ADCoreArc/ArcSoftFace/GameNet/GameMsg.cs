using ArcFaceSDK.Entity;
using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem.ADCoreModel;
using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.GameNet
{
    public class GameMsg
    {
        public CMD cmd;
        public ErrorType errorType;
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public Rsp_GetKeyByPersonImport rsp_GetKey;
        public Req_Verification req_Verification;
        public Rsp_Verfication rsp_Verfication;
        public Req_New_Import req_New_Import;
        public Rsp_New_Import rsp_New_Import;

        public Req_Add_Import req_Add_Import;
        public Rsp_Add_Import rsp_Add_Import;
        public Req_AdminChangeUserPassword req_AdminChangeUserPassword;
        public Rsp_AdminChangeUserPassword rsp_AdminChangeUserPassword;
        public Req_AdminChangeAdminPassword req_AdminChangeAdminPassword;
        public Rsp_AdminChangeAdminPassword rsp_AdminChangeAdminPassword;
        public Req_UserChangeUserPassword req_UserChangeUserPassword;
        public Rsp_UserChangeUserPassword rsp_UserChangeUserPassword;
        public Rsp_ExportGrade rsp_ExportGrade;
        public Req_NewGroupGetGroup req_NewGroupGetGroup;
        public Rsp_NewGroupGetGroup rsp_NewGroupGetGroup;
        public Rsp_QueryExaminationTime rsp_QueryExaminationTime;
        public Req_NewGroupUpdateUserExcel req_NewGroupUpdateUserExcel;
        public Rsp_NewGroupUpdateUserExcel rsp_NewGroupUpdateUserExcel;
        public Req_NewGroupFaceRegister req_NewGroupFaceRegister;
        public Rsp_NewGrroupFaceRegister rsp_NewGroupFaceRegister;
        public Req_GetGroupMent req_GetGroupMent;
        public Rsp_GetGroupMent rsp_GetGroupMent;

        public Req_TestNumberInquriry req_TestNumberInquriry;
        public Rsp_TestNumberInquriey rsp_TestNumberInquriey;
        public Req_GetCurrentGroupMsg req_GetCurrentGroupMsg;
        public Rsp_GetCurrentGroupMsg rsp_GetCurrentGroupMsg;
        public ReqModify_Grades reqModify_Grades;
        public RspModify_Grades rspModify_Grades;

        public Req_GetFaceFeature req_GetFaceFeature;
        public Rsp_GetFaceFeature rsp_GetFaceFeature;
        public Req_DelectFaceData req_DelectFaxeData;
        public Rsp_DelectFaceData rsp_DelectFaceData;
        public  Req_NewGroupUpdateUserExcelByFile req_NewGroupUpdateUserExcelByFile;
        public Rsp_NewGroupUpdateUserExcelByFile rsp_NewGroupUpdateUserExcelByFile;
    }

    public class Rsp_NewGroupUpdateUserExcelByFile
    {
        public int IsSucess { get; set; }
    }

    public class Req_NewGroupUpdateUserExcelByFile
    {
        public List<UserExcel > userExcelModes { get;set;}
    }

    public class Rsp_DelectFaceData
    {
       public   int   IsSucess { get; set; }
    }

    public class Req_DelectFaceData
    {
        public string groupID { get; set; }
        public string Name { get; set; }
        public FaceFeature faceFeature { get; set; }
    }

    /// <summary>
    ///  人脸注册的返回
    /// </summary>
    public class Rsp_NewGrroupFaceRegister
    {
        public  bool  Issucess { get; set; }  
    }
    /// <summary>
    /// 人脸注册的请求
    /// </summary>
    public class Req_NewGroupFaceRegister
    {
        public  List<StudentFaceData> faces { get; set; }
    }

    public class Rsp_GetFaceFeature
    {
       public   List<FaceDataMode > faceDataModes { get; set; }
    }

    public class Req_GetFaceFeature
    {
        public   string groupID { get; set; } // 根据组号去获取人脸信息

        
    }

    public class RspModify_Grades
    {
        public UserExcelMode userExcelMode { get; set; }
    }

    public class ReqModify_Grades
    {
        public UserExcelMode userExcelModes { get; set; }
    }

    public class Rsp_GetCurrentGroupMsg
    {
        public List<UserExcelMode> userExcelModes { get; set; }
    }

    public class Req_GetCurrentGroupMsg
    {
        public string examTime { get; set; }
        public string groupNum { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Rsp_TestNumberInquriey
    {
        public List<UserExcelMode> userExcelModes;

    }
    /// <summary>
    /// 
    /// </summary>
    public class Req_TestNumberInquriry
    {
        public string examNum { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>

    public class Rsp_GetGroupMent
    {
        public List<GroupDataModel> _groupDataModels;
    }

    public class Req_GetGroupMent
    {
        public string examTime { get; set; }
    }

    public class Rsp_NewGroupUpdateUserExcel
    {
        public int IsSucess { get; set; }
    }

    public class Req_NewGroupUpdateUserExcel
    {
        public List<UserExcel> userExcelModes;
    }

    public class Rsp_QueryExaminationTime
    {
        public List<StartTestingExamDataModel> startTestingExamDataModels { get; set; }
    }

    public class Rsp_NewGroupGetGroup
    {
        public bool IsCanCreatGroup { get; set; }
    }

    public class Req_NewGroupGetGroup
    {
        public string groupID { get; set; }
    }

    public class Rsp_Add_Import
    {
        public int IsReq_Add_Import { get; set; }
    }

    public class Req_Add_Import
    {
        public List<UserExcel> userExcels { get; set; }
    }

    public class Rsp_New_Import
    {
        public int IsReq_New_Import { get; set; }
    }

    public class Req_New_Import
    {
        public List<UserExcel> ListUserExcel { get; set; }
    }

    public class Rsp_ExportGrade
    {
        public List<UserExcel> userExcel { get; set; }
    }

    public class Rsp_UserChangeUserPassword
    {
        public int IsSucess { get; set; }
    }

    public class Req_UserChangeUserPassword
    {
        public String account { get; set; }
        public String oldPassword { get; set; }
        public String newPassword { get; set; }
    }

    public class Rsp_AdminChangeAdminPassword
    {
        public int IsSucess { get; set; }
    }

    public class Rsp_AdminChangeUserPassword
    {
        public int IsSucess { get; set; }
    }

    public class Req_AdminChangeAdminPassword
    {
        public string account { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class Req_AdminChangeUserPassword
    {
        public string account { set; get; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class Rsp_Verfication
    {
        public int IsVerfication { get; set; }
    }

    public class Req_Verification
    {
        public string pass { get; set; }
    }

    public class Rsp_GetKeyByPersonImport
    {
        public String number { get; set; }
    }

    public class RspLogin
    {
        public PlayerData playerData { get; set; }
    }
    public class PlayerData
    {
        public int isLoginSucess { get; set; }
    }

    public class ReqLogin
    {
        public string acc { get; set; }
        public string password { get; set; }
    }
}