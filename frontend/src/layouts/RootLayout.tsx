import { Link, Outlet } from "react-router-dom";

export const RootLayout = () => {
  return (
    <div>
      <header>
        <nav>
          <Link to="/articles">Home</Link>
          <Link to="/articles/new">New Article</Link>
        </nav>
      </header>

      <main>
        <Outlet />
      </main>
    </div>
  );
};
