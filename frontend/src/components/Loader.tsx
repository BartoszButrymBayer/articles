import styles from "./Loader.module.css";

export const Loader = () => {
  return (
    <div className={styles.overlay}>
      <div className={styles.loader} />
    </div>
  );
};
