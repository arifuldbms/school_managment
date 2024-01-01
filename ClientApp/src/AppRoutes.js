import { TeacherList } from "./components/TeacherList";
import { Departments } from "./components/Departments";
import { Library } from "./components/Library";
import { Lab } from "./components/Lab";
import { Collegeresult } from "./components/Collegeresult";
import CRUD from "./components/CRUD";
import ExcelUpload from "./components/ExcelUpload";
import Error from "./components/Error";

import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
      path: '/TeacherList',
      element: <TeacherList />
  },
  {
      path: '/Departments',
      element: <Departments />
    },

    {
        path: '/Library',
        element: <Library />
    },

    {
        path: '/Lab',
        element: <Lab />
    },

    {
        path: '/Collegeresult',
        element: <Collegeresult />
    },

    {
        path: '/crud',
        element: <CRUD />
    },

    {
        path: '/ExcelUpload',
        element: <ExcelUpload />
    },


    {
        path: '/*',
        element: <Error />
    }
];

export default AppRoutes;
