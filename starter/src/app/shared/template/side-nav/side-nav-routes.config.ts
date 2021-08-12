import { SideNavInterface } from '../../interfaces/side-nav.type';
export const ROUTES: SideNavInterface[] = [
  {
    path: '',
    title: 'Dashboard',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'dashboard',
    submenu: []
  },
  {
    path: '',
    title: 'Danh Sách Chức Năng',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'appstore',
    submenu: [
      {
        path: 'user-list',
        title: 'Danh sách tài khoản',
        iconType: 'nzIcon',
        iconTheme: 'outline',
        icon: 'user',
        submenu: []
      }

    ]
  },
  {
    path: '',
    title: 'Cài đặt',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'user',
    submenu: [
      {
        path: 'danh-muc-ten-quyen',
        title: 'Danh mục tên quyền',
        iconType: 'nzIcon',
        iconTheme: 'outline',
        icon: 'usergroup-add',
        submenu: []
      },
      {
        path: 'nguoi-dung',
        title: 'Người Dùng',
        iconType: 'nzIcon',
        iconTheme: 'outline',
        icon: 'usergroup-add',
        submenu: []
      }

    ]
  },
]
