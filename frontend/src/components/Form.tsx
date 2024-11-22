import { SubmitHandler, useForm } from "react-hook-form";
import { FieldValues } from "../interfaces/article";
import styles from "./Form.module.css";
import { useEffect } from "react";

const defaultValues = {
  title: "",
  author: "",
  shortDescription: "",
  fullContent: "",
  tags: "",
};

export const Form = ({
  onSubmit,
  heading,
  buttonTitle,
  preloadedValues,
}: {
  onSubmit: SubmitHandler<FieldValues>;
  heading: string;
  buttonTitle: string;
  preloadedValues?: FieldValues;
}) => {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitSuccessful },
  } = useForm<FieldValues>({
    defaultValues: preloadedValues ?? defaultValues,
  });

  useEffect(() => {
    reset();
  }, [isSubmitSuccessful, reset]);

  return (
    <div>
      <h2 className={styles.h2}>{heading}</h2>

      <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
        <div>
          <label>Title</label>
          <input {...register("title", { required: true })} />
          {errors.title && <span>This field is required</span>}
        </div>
        <div>
          <label>Author</label>
          <input {...register("author", { required: true })} />
          {errors.author && <span>This field is required</span>}
        </div>
        <div>
          <label>Short description</label>
          <input {...register("shortDescription", { required: true })} />
          {errors.shortDescription && <span>This field is required</span>}
        </div>
        <div>
          <label>Full content</label>
          <textarea {...register("fullContent", { required: true })} />
          {errors.fullContent && <span>This field is required</span>}
        </div>
        <div>
          <label>Tags</label>
          <input {...register("tags", { required: true })} />
          {errors.tags && <span>This field is required</span>}
        </div>

        <button type="submit" className={styles.submit}>
          {buttonTitle}
        </button>
      </form>
    </div>
  );
};
