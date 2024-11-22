import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { SubmitHandler } from "react-hook-form";
import { FieldValues } from "../interfaces/article";
import { getArticle, updateArticle } from "../services/articles";
import { Form } from "../components/Form";
import { Loader } from "../components/Loader";

export const UpdateArticle = () => {
  const { articleId } = useParams();
  const [article, setArticle] = useState<FieldValues | null>(null);

  const fetchArticle = useCallback(async () => {
    if (articleId) {
      const data = await getArticle(articleId);
      setArticle(data);
    }
  }, [articleId]);

  const onSubmit: SubmitHandler<FieldValues> = async (data) => {
    const d = new Date();
    const now = `${d.getFullYear()}-${d.getMonth()}-${d.getDate()}`;

    const tags = Array.isArray(data.tags) ? data.tags : data.tags.split(", ");

    const updatedArticle = {
      ...data,
      publishedDate: now,
      tags: tags,
    };

    await updateArticle(articleId!, updatedArticle);
    await fetchArticle();
  };

  useEffect(() => {
    fetchArticle();
  }, [fetchArticle]);

  if (!article) {
    return <Loader />;
  }

  return (
    <Form
      onSubmit={onSubmit}
      heading="Update an article"
      buttonTitle="Update"
      preloadedValues={article}
    />
  );
};
