const handleFilterUrl = (url, filters) => {
  if (filters) {
    Object.keys(filters).forEach((key) => {
      if (filters[key]) {
        url += `&${key}=${filters[key]}`
      }
    })
  }
  return url
}
const path = {
  admin: {
    getPermissions: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `Permissons/get-permissions?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    getUserPermissions: ({userId, pageIndex, pageSize}) =>
      `Permissons/get-user-permissions/${userId}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    changeUserPermissions: ({userId}) =>
      `Permissons/edit-user-permissions/${userId}`,
    getRolePermissions: ({roleId, pageIndex, pageSize}) =>
      `Permissons/get-role-permissions/${roleId}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    changRolePermissions: ({roleId}) =>
      `Permissons/edit-role-permissions/${roleId}`,
  },
  auth: {
    login: `Users/login`,
    logout: `/logout`,
    changePassword: ({userId}) => `Users/change-password/${userId}`,
    refreshToken: ({token}) => `Users/refresh-token?token=${token}`,
    forgotPassword: ({email}) => `Users/forgot-password?email=${email}`,
  },
  dashboard: {
    getStatisticCandidates: ({departmentId}) => {
      return departmentId != null
        ? `Statistic/candidates/by-department?departmentId=${departmentId}`
        : `Statistic/candidates/by-department`
    },
    getStatisticUsers: ({departmentId}) => {
      return departmentId != null
        ? `Statistic/users/by-department?departmentId=${departmentId}`
        : `Statistic/users/by-department`
    },
    getStatisticJobPosts: ({departmentId}) => {
      return departmentId != null
        ? `Statistic/job-posts/by-department?departmentId=${departmentId}`
        : `Statistic/job-posts/by-department`
    },
    getStatisticOkr: ({departmentId}) => {
      return departmentId != null
        ? `Statistic/okr/by-department?departmentId=${departmentId}`
        : `Statistic/okr/by-department`
    },
    getStatisticOkrRequest: ({departmentId}) => {
      return departmentId != null
        ? `Statistic/okr-request/by-department?departmentId=${departmentId}`
        : `Statistic/okr-request/by-department`
    },
    getStatisticCandidateOfPost: ({departmentId}) => {
      return departmentId != null
        ? `Statistic/candidate-of-post/by-department?departmentId=${departmentId}`
        : `Statistic/candidate-of-post/by-department`
    },
  },
  okr: {
    getOkrDepart: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `Okrs/by-department?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    editOkrs: ({okrId}) => `Okrs/${okrId}/approveStatus`,
    getOkrs: (pageIndex, pageSize, departmentId, filters) => {
      return handleFilterUrl(
        `Okrs/by-department?pageIndex=${pageIndex}&pageSize=${pageSize}&departmentId=${departmentId}`,
        filters,
      )
    },
    getOkrDetail: ({okrId}) => `Okrs/${okrId}`,
    getOkrActivity: ({okrId}) => `OkrHistories/${okrId}`,
    addOkrActivity: ({okrId}) => `OkrHistories/${okrId}`,
    CreateOkr: `Okrs`,
    getOkrRequest: (pageIndex, pageSize, filters, departmentId) => {
      return handleFilterUrl(
        `Okrs/requests?pageIndex=${pageIndex}&pageSize=${pageSize}&departmentId=${departmentId}`,
        filters,
      )
    },
    addOkrHistoryComment: ({okrId}) => `OkrHistories/${okrId}/comments`,
    deleteOkrHistoryComment: ({okrHistoryId}) =>
      `OkrHistories/comments/${okrHistoryId}`,
    editProgressOkr: ({okrId}) => `Okrs/${okrId}/UpdateProgressOkr`,
    editOwnerOkr: ({okrId}) => `Okrs/${okrId}/UpdateOwnerOkr`,
    editOkrRequest: ({okrId}) => `Okrs/${okrId}/UpdateOkrRequest`,
  },
  post: {
    getPosts: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `JobPost?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    addPost: 'JobPost',
    editPost: ({postId}) => `JobPost/${postId}`,
  },
  homepage: {
    getPosts: (pageIndex, pageSize) =>
      `JobPost?pageIndex=${pageIndex}&pageSize=${pageSize}&status=Recruiting`,
    appylyCV: () => `Candidates`,
    homePageSetting: () => `HomePages`,
    getHomePageInfo: (pageIndex, pageSize) =>
      `HomePages?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    updateHomePageStatus: ({id}) => `HomePages/${id}/Activate`,
    getHomePageActive: () => `HomePages/GetHomePageActive`,
    editHomePage: ({id}) => `HomePages/${id}`,
    getLastestVersion: () => {
      let url = `HomePages/GetLatestVersion`
      return url
    },
    getImageHomePage: ({id}) => `/HomePages/download-image/${id}`,
  },
  reason: {
    addReasons: 'HomePageReasons',
    getReasons: () => `HomePageReasons`,
    editReasons: ({reasonId}) => `HomePageReasons/${reasonId}`,
    deleteReason: ({reasonId}) => `HomePageReasons/${reasonId}`,
  },
  candidate: {
    getCandidates: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `candidates?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    updateStatus: ({candidateId}) => `candidates/${candidateId}`,

    getCandidateCV: (candidateId) => `candidates/${candidateId}/cv-detail`,
  },
  employee: {
    getEmployees: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `UserInformations?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    getEmployeeInformation: (userId) => `UserInformations/user/${userId}`,
    addEmployee: `Users`,
    editEmployee: `UserInformations`,
    getEmployeeHistories: (userId) => `UserHistories/${userId}`,
    getEmployeeFamily: (userId) => `UserInformations/family/user/${userId}`,
    addEmployeeFamily: (userId) => `UserInformations/family/user/${userId}`,
    editEmployeeFamily: (userId, memberId) =>
      `UserInformations/family/user/${userId}?memberId=${memberId}`,
    updateStatusEmployee: ({userId}) => `UserInformations/${userId}/status`,
    updatePositionEmployee: `Users/change-role-department`,
    deleteEmployeeFamily: (memberId) =>
      `UserInformations/user-family/${memberId}`,
  },
  department: {
    getDepartments: () => {
      let url = `Departments`
      return url
    },
    getDepartmentFilter: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `Departments?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    addDepartment: `Departments`,
    editDepartment: ({departmentId}) => `Departments/${departmentId}`,
  },
  role: {
    getRoles: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `Roles?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    addRole: `Roles`,
    deleteRole: (roleId) => `Roles?id=${roleId}`,
  },
  user: {
    addNewUser: 'Users',
    getUserInformation: ({userId}) => `UserInformations/personal/${userId}`,
    editUserInformation: ({userId}) => `UserInformations/personal/${userId}`,
    getAllUser: (pageIndex, pageSize) => {
      return handleFilterUrl(
        `Users?pageIndex=${pageIndex}&pageSize=${pageSize}`,
      )
    },
    getUserInformationByDepart: (pageIndex, pageSize, departmentId) => {
      return handleFilterUrl(
        `UserInformations/by-department-id?departmentId=${departmentId}&pageIndex=${pageIndex}&pageSize=${pageSize}`,
      )
    },
    getUser: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `Users?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
  },
  userGroup: {
    getUserGroup: (pageIndex, pageSize, filters) => {
      return handleFilterUrl(
        `UserGroups?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        filters,
      )
    },
    editUserGroup: ({userGroupId}) => `UserGroups/${userGroupId}`,
    addUserGroup: `UserGroups`,
    deleteUserGroup: (userGroupId) => `UserGroups/${userGroupId}`,
  },
}
export default path
