import axios from "./axios";
import { Article } from "../interfaces/article";

export const getArticles = async () => {
  try {
    const response = await axios.get("/articles");

    if (response.status === 200) {
      return response.data;
    }

    console.error("Could not fetch articles");

    return [];
  } catch (error) {
    console.error("Something went wrong: " + error);
  }
};

export const getArticle = async (articleId: string) => {
  try {
    const response = await axios.get(`/articles/${articleId}`);

    if (response.status === 200) {
      return response.data;
    }

    console.error(`Could not fetch article with id ${articleId}`);

    return null;
  } catch (error) {
    console.error("Something went wrong: " + error);
  }
};

export const createArticle = async (article: Article) => {
  try {
    const response = await axios.post("/articles", article);

    if (response.status !== 201) {
      console.error("Could not save new article");
    }
  } catch (error) {
    console.error("Something went wrong: " + error);
  }
};

export const updateArticle = async (
  articleId: string,
  updatedArticle: Article
) => {
  try {
    const response = await axios.put(`/articles/${articleId}`, updatedArticle);

    if (response.status !== 204) {
      console.error("Could not update article");
    }
  } catch (error) {
    console.error("Something went wrong: " + error);
  }
};

export const deleteArticle = async (articleId: string) => {
  try {
    const response = await axios.delete(`/articles/${articleId}`);

    if (response.status !== 204) {
      console.error("Could not delete article");
    }
  } catch (error) {
    console.error("Something went wrong: " + error);
  }
};
