import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { getArticle } from "../services/articles";
import { GetArticle } from "../interfaces/article";
import styles from "./Article.module.css";

export const Article = () => {
  const { articleId } = useParams();
  const [article, setArticle] = useState<GetArticle | null>(null);

  useEffect(() => {
    const fetchArticle = async () => {
      if (articleId) {
        const data = await getArticle(articleId);
        setArticle(data);
      }
    };

    fetchArticle();
  }, [articleId]);

  if (!article) {
    return <p>☕️ Loading...</p>;
  }

  return (
    <div className={styles.container}>
      <h1>{article?.title}</h1>
      <h4>{article.author}</h4>
      <p>{article.fullContent}</p>
      <div className={styles.link}>
        <Link to={`/articles/update/${articleId}`}>Edit</Link>
      </div>
    </div>
  );
};
