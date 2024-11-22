import { SubmitHandler } from "react-hook-form";
import { createArticle } from "../services/articles";
import { FieldValues } from "../interfaces/article";
import { Form } from "../components/Form";

export const NewArticle = () => {
  const onSubmit: SubmitHandler<FieldValues> = async (data) => {
    const d = new Date();
    const now = `${d.getFullYear()}-${d.getMonth()}-${d.getDate()}`;

    const tags = data.tags.split(", ");

    const updatedArticle = {
      ...data,
      publishedDate: now,
      tags: tags,
    };

    await createArticle(updatedArticle);
  };

  return (
    <Form onSubmit={onSubmit} heading="Add new article" buttonTitle="Create" />
  );
};
