import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { deleteArticle, getArticles } from "../services/articles";
import { GetArticle } from "../interfaces/article";
import { Loader } from "../components/Loader";
import TrashIcon from "../assets/trash.svg";
import styles from "./Articles.module.css";

export const Articles = () => {
  const [articles, setArticles] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  const getData = async () => {
    const data = await getArticles();
    setArticles(data);
  };

  const removeArticle = async (id: string) => {
    setIsLoading(true);
    await deleteArticle(id);
    await getData();
    setIsLoading(false);
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
      <div>
        <h1 className={styles.heading}>List of Agricultural Articles</h1>
        <ul className={styles.list}>
          {articles.map((article: GetArticle) => (
            <li key={article.articleID} className={styles.container}>
              <h2>{article.title}</h2>
              <h4>{article.author}</h4>
              <p>{article.shortDescription}</p>
              <div className={styles.footer}>
                <Link to={`/articles/${article.articleID}`}>Read more...</Link>
                <button
                  type="button"
                  onClick={() => removeArticle(article.articleID)}
                >
                  <img
                    src={TrashIcon}
                    width={25}
                    height={25}
                    alt="remove article"
                  />
                </button>
              </div>
            </li>
          ))}
        </ul>
      </div>
      {isLoading && <Loader />}
    </>
  );
};
