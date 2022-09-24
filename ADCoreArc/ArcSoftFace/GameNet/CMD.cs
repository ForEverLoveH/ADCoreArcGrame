namespace ArcSoftFace.GameNet
{
    public enum CMD
    {
        None,
        ReqLogin,
        RspLogin,
        Req_GetKeyByPersonImport,
        Rsp_GetKeyByPersonImport,
        Req_Verification,
        Rsp_Verification,

        Req_AdminChangeUserPassword,
        Rsp_AdminChangeAdminPassword,
        Req_AdminChangeAdminPassword,
        Rsp_AdminChangeUserPassword,

        Rsp_UserChangeUserPassword,
        Req_UserChangeUserPassword,

        Req_ExportGrade,
        Rsp_ExportGrade,
        Req_New_Import,
        Rsp_New_Import,
        Req_Add_Import,
        Rsp_Add_Import,

        Req_NewGroupGetGroup,
        Rsp_NewGroupGetGroup,
        Req_QueryExaminationTime,
        Rsp_QueryExaminationTime,

        Req_NewGroupUpdateUserExcel,
        Rsp_NewGroupUpdateUserExcel,
        Req_GetGroupMent,
        Rsp_GetGroupMent,
        Req_TestNumberInquriry,
        Rsp_TestNumberInquiry,
        Req_GetCurrentGroupMsg,
        Rsp_GetCurrentGroupMsg,
        ReqModify_Grades,
        RspModify_Grades,
        Req_GetKeyByExportGrade,
        Rsp_GetKeyByExportGrade


    }
}