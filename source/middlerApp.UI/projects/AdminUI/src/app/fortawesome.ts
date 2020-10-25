import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faAddressCard, faAngleDoubleRight, faChevronLeft, faChevronRight, faCube, faCubes, faDesktop, 
  faEllipsisV, faStream, faToggleOff, faToggleOn, faUser, faUsers, faUsersCog, faUserTag, faKey } from '@fortawesome/free-solid-svg-icons';
import { faAddressCard as farAddressCard, faPlayCircle, faPlusSquare, faStopCircle } from '@fortawesome/free-regular-svg-icons';
import {
  DashboardOutline, FolderOutline, SettingOutline, FieldBinaryOutline, DeleteOutline,
  ContainerOutline, OrderedListOutline, CodeOutline, FormOutline, RollbackOutline, SyncOutline,
  UserAddOutline, LoginOutline, BugOutline
} from '@ant-design/icons-angular/icons';
export class FortAwesomeLib {
  Init(library: FaIconLibrary) {

    // library.addIconPacks(fas, far);
    library.addIcons(
      faStream,
      faUser,
      faUserTag,
      faDesktop,
      faCube,
      faCubes,
      faAddressCard,
      farAddressCard,
      faEllipsisV,
      faToggleOff,
      faToggleOn,
      faPlayCircle,
      faUsersCog,
      faPlusSquare,
      faUsers,
      faAngleDoubleRight,
      faChevronLeft,
      faChevronRight,
      faKey,
      faStopCircle
    )

  }

  static AntdIcons = [
    DashboardOutline,
    FolderOutline,
    SettingOutline,
    FieldBinaryOutline,
    DeleteOutline,
    ContainerOutline, OrderedListOutline, CodeOutline, FormOutline, RollbackOutline,
    SyncOutline, UserAddOutline,
    LoginOutline,
    BugOutline
  ]

}