export type Article = {
  title: string;
  shortDescription: string;
  fullContent: string;
  author: string;
  publishedDate: string;
  tags: string[];
};

export type GetArticle = Article & { articleID: string };

export type FieldValues = {
  title: string;
  shortDescription: string;
  fullContent: string;
  author: string;
  publishedDate: string;
  tags: string;
};
