import React from 'react'
import {Route, Routes, Navigate} from 'react-router-dom'
import CandidatePage from '../pages/recruitment/candidate/list/CandidatePage'
import EmployeeManagementPage from '../pages/employee/list/EmployeeManagementPage'
import JobPostPage from '../pages/recruitment/job/list/JobPostPage'
import SettingPage from '../pages/setting/SettingPage'
import OkrPage from '../pages/okr/OkrManagementPage'
import EditEmployeePage from '../pages/employee/infomation/edit/EditEmployeePage'
import RequestTab from '../pages/okr/requestTab/RequestTab'
import Login from '../pages/auth/login/Login'
import NotFoundPage from '../pages/errorPage/NotFoundPage'
import Dashboard from '../pages/dashboard/DashBoard'
import PrivateRoutes from './PrivateRouters'
import ForgetPassword from '../pages/auth/forgetPassword/ForgetPassword'
import HomePage1 from '../pages/homepage/HomePage1'
import EditPermissionPage from '../pages/admin/permission/edit/EditPermissionPage'
import EditPermissionRolePage from '../pages/admin/permission/edit/EditPermissionRolePage'
import DepartmentTab from '../pages/admin/generalsetting/DepartmentTab'
import RoleTab from '../pages/admin/generalsetting/RoleTab'
import PermissionTab from '../pages/admin/permission/PermissionTab'
import UserGroupTab from '../pages/admin/generalsetting/UserGroupTab'
import ReasonPageTab from '../pages/admin/homepagesetting/ReasonPageTab'
import HomePageTab from '../pages/admin/homepagesetting/HomePageTab'
function Routers() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/homepage" />} />
      <Route path="/homepage" element={<HomePage1 />} />

      <Route path="/login" element={<Login />} />
      <Route path="/forgetPassword" element={<ForgetPassword />} />

      <Route element={<PrivateRoutes requiredPermissions={['Common']} />}>
        <Route path="/dashboard" element={<Dashboard />} />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['JobPost:List']} />}>
        <Route path="/jobpost" element={<JobPostPage />} />
      </Route>
      <Route
        element={<PrivateRoutes requiredPermissions={['Employee:List']} />}
      >
        <Route path="/user" element={<EmployeeManagementPage />} />
      </Route>

      <Route element={<PrivateRoutes requiredPermissions={['Common']} />}>
        <Route path="/account" element={<SettingPage />} />
      </Route>

      <Route element={<PrivateRoutes requiredPermissions={['Okr:List']} />}>
        <Route path="/okr" element={<OkrPage />} />
      </Route>

      <Route
        element={<PrivateRoutes requiredPermissions={['Employee:Edit']} />}
      >
        <Route path="/user/edit/:userId" element={<EditEmployeePage />} />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Common']} />}>
        <Route path="/user/detail/:userId" element={<EditEmployeePage />} />
      </Route>

      <Route
        element={<PrivateRoutes requiredPermissions={['OkrRequest:List']} />}
      >
        <Route path="/okrequest" element={<RequestTab />} />
      </Route>

      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route path="/department" element={<DepartmentTab />} />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route path="/role" element={<RoleTab />} />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route path="/userPermission" element={<PermissionTab />} />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route path="/userGroup" element={<UserGroupTab />} />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route path="/homepagesetting" element={<HomePageTab />} />
      </Route>

      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route
          path="/userPermission/permission/:userId"
          element={<EditPermissionPage />}
        />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route path="/homepagereason" element={<ReasonPageTab />} />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route
          path="/role/rolePermission/:roleId"
          element={<EditPermissionRolePage />}
        />
      </Route>
      <Route element={<PrivateRoutes requiredPermissions={['Admin']} />}>
        <Route
          path="/role/rolePermission/:roleId"
          element={<EditPermissionRolePage />}
        />
      </Route>
      <Route
        element={<PrivateRoutes requiredPermissions={['Candidate:List']} />}
      >
        <Route
          path="/candidate/jobpost/:jobpostId"
          element={<CandidatePage />}
        />
      </Route>
      <Route
        element={<PrivateRoutes requiredPermissions={['Candidate:List']} />}
      >
        <Route path="/candidate" element={<CandidatePage />} />
      </Route>

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  )
}

export default Routers
