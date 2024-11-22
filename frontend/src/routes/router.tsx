import { createBrowserRouter, Navigate } from "react-router-dom";
import { RootLayout } from "../layouts/RootLayout";
import { Articles } from "../pages/Articles";
import { Article } from "../pages/Article";
import { NewArticle } from "../pages/NewArticle";
import { UpdateArticle } from "../pages/UpdateArticle";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: (
      <div>
        <h1>✋ Page Not Found</h1>
      </div>
    ),
    children: [
      {
        index: true,
        element: <Navigate to="/articles" />,
      },
      {
        path: "/articles",
        element: <Articles />,
      },
      {
        path: "/articles/:articleId",
        element: <Article />,
      },
      {
        path: "/articles/new",
        element: <NewArticle />,
      },
      {
        path: "/articles/update/:articleId",
        element: <UpdateArticle />,
      },
    ],
  },
  {
    path: "*",
    element: (
      <div>
        <h1>✋ Page Not Found</h1>
      </div>
    ),
  },
]);
