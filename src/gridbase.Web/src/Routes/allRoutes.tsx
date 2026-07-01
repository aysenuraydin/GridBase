import React from "react";

//AuthenticationInner pages
import BasicSignIn from '../pages/AuthenticationInner/Login/BasicSignIn';
import BasicSignUp from '../pages/AuthenticationInner/Register/BasicSignUp';
import BasicPasswReset from '../pages/AuthenticationInner/PasswordReset/BasicPasswReset';
import BasicLockScreen from '../pages/AuthenticationInner/LockScreen/BasicLockScr';
import BasicLogout from '../pages/AuthenticationInner/Logout/BasicLogout';
import BasicSuccessMsg from '../pages/AuthenticationInner/SuccessMessage/BasicSuccessMsg';
import BasicTwosVerify from '../pages/AuthenticationInner/TwoStepVerification/BasicTwosVerify';
import Basic404 from '../pages/AuthenticationInner/Errors/Basic404';
import Alt404 from '../pages/AuthenticationInner/Errors/Alt404';
import Error500 from '../pages/AuthenticationInner/Errors/Error500';
import BasicPasswCreate from "../pages/AuthenticationInner/PasswordCreate/BasicPasswCreate";
import Offlinepage from "../pages/AuthenticationInner/Errors/Offlinepage";

//login
import Login from "pages/Authentication/Login";
import Register from "pages/Authentication/Register";

// User Profile
import UserProfile from "../pages/Authentication/user-profile";
import { MainMenuItems } from "pages/MenuItems";
import { ForgetPasswordPage } from "pages/Authentication/ForgetPassword"; 
import { Profile } from "pages/Profile";
import Team from "pages/Team/Team";
import SearchResults from "pages/SearchResults/SearchResults";
import TermsCondition from "pages/TermsCondition";
import PrivacyPolicy from "pages/PrivacyPolicy";
import { UserManagementPage } from "pages/Users";
import { RoleManagementPage } from "pages/Roles";
import Documents from "pages/Documents";
import { ProfileSettings } from "pages/Profile/ProfileSettings";
import OnePage from "pages/Landing";
import { TenantSettings } from "pages/Tenant";
import { FeatureGuard } from "./FeatureGuard";
import { Dashboard } from "pages/Dashboard";
import Gallery from "pages/Gallery/Gallery";
import { MainFaqsPage } from "pages/Faqs/Faqs";
import Contacts from "pages/Contacts/Contacts";
import About from "pages/About";
import Basic403 from "pages/AuthenticationInner/Errors/Basic403";
import ConsolePage from "pages/ConsolePage";
import StoragePage from "pages/StoragePage";
import { ProjectsPage } from "pages/Projectspage";
import ApiKeysPage from "pages/ApikeysPage";
import DashboardPage from "pages/Dashboard/DashboardPage";
import ProjectSettingsPage from "pages/ProjectSettingsPage";
import RequireProject from "./RequireProject";
import { MainDatatables } from "pages/Datatables";
import { MainDatatableItem } from "pages/DatatableItem";
import { MainCreateOrUpdatePage } from "pages/DatatableItem/CreateRow/components/MainCreateOrUpdatePage";

const withGuard = (component: React.ReactNode) => (
  <FeatureGuard>
    {component}
  </FeatureGuard>
);
const withGuardByRoles = (component: React.ReactNode, allowedRoles: string[]) => (
  <FeatureGuard allowedRoles={[...allowedRoles]}>
    {component}
  </FeatureGuard>
);

const ADMIN_ROLES = ["GB"];

const authProtectedRoutes = [
  // ── Proje-gerektiren sayfalar (RequireProject ile sarılı) ──
  { path: "/overview", component: <RequireProject><DashboardPage /></RequireProject> },
  { path: "/datatables", component: <RequireProject><MainDatatables /></RequireProject> },
  { path: "/datatable/:id", component: <RequireProject><MainDatatableItem /></RequireProject> },
  { path: "/datatable-view/:id", component: <RequireProject><MainCreateOrUpdatePage /></RequireProject> },
  { path: "/console", component: <RequireProject><ConsolePage /></RequireProject> },
  { path: "/storage", component: <RequireProject><StoragePage /></RequireProject> },
  { path: "/keys", component: <RequireProject><ApiKeysPage /></RequireProject> },
  { path: "/project-settings", component: <RequireProject><ProjectSettingsPage /></RequireProject> },

  // ── Proje seçimi (guard YOK) ──
  { path: "/projects", component: <ProjectsPage /> },

  // ── Admin ──
  { path: "/menuItems", component: withGuardByRoles(<MainMenuItems />, ADMIN_ROLES) },
  { path: "/users", component: withGuardByRoles(<UserManagementPage />, ADMIN_ROLES) },
  { path: "/roles", component: withGuardByRoles(<RoleManagementPage />, ADMIN_ROLES) },

  // ── Profil ──
  { path: "/user-profile", component: <UserProfile /> },
  { path: "/profile/:id", component: <Profile /> },
  { path: "/profile-settings", component: withGuard(<ProfileSettings />) },

  // ── Diğer ──
  { path: "/documents", component: <Documents /> },
  { path: "/team", component: <Team /> },
  { path: "/settings", component: withGuardByRoles(<TenantSettings />, ["GB"]) },
  { path: "/dashboard", exact: true, component: <Dashboard /> },

  { path: "/", exact: true, component: <FeatureGuard /> },
  { path: "*", component: <FeatureGuard /> },
];

const publicRoutes = [
  { path: "/login", component: <Login /> },
  { path: "/forgot-password", component: <ForgetPasswordPage /> },
  { path: "/register", component: <Register /> },
  { path: "/landing", component: <OnePage /> },

  //AuthenticationInner pages
  { path: "/auth-signin-basic", component: <BasicSignIn /> },
  { path: "/auth-signup-basic", component: <BasicSignUp /> },
  { path: "/auth-pass-reset-basic", component: <BasicPasswReset /> },
  { path: "/auth-lockscreen-basic", component: <BasicLockScreen /> },
  { path: "/auth-logout-basic", component: <BasicLogout /> },
  { path: "/auth-success-msg-basic", component: <BasicSuccessMsg /> },
  { path: "/auth-twostep-basic", component: <BasicTwosVerify /> },
  { path: "/auth-404-basic", component: <Basic404 /> },
  { path: "/auth-404-alt", component: <Alt404 /> },
  { path: "/auth-500", component: <Error500 /> },
  { path: "/auth-pass-change-basic", component: <BasicPasswCreate /> },
  { path: "/auth-offline", component: <Offlinepage /> },
];

const publicLayoutRoutes = [
  { path: "/forbidden", component: <Basic403 /> },
  { path: "/about", component: <About /> },
  { path: "/gallery", component: <Gallery /> },
  { path: "/faqs", component: <MainFaqsPage /> },
  { path: "/contacts", component: <Contacts /> },
  { path: "/privacy-policy", component: <PrivacyPolicy /> },
  { path: "/terms-condition", component: <TermsCondition /> },
  { path: "/search-results", component: <SearchResults /> },
];

export { authProtectedRoutes, publicRoutes, publicLayoutRoutes };