import { useState } from "react";
import styles from "./SearchResult.module.scss";
import classNames from "classnames/bind";
import Phone from "./Phone";

const cx = classNames.bind(styles);

function SearchResult({ results, inputRef, show }) {
  const handleResults = (e) => {
    inputRef.current.focus();
  };
  return (
    <div
      className={cx("wrapper", { show: show })}
      onClick={(e) => handleResults(e)}
    >
      <div className={cx("result-searching")}>
        {results.map((result, id) => {
          return result.phones.map((phone) => {
            return <Phone key={id} phone={phone} />;
          });
        })}
      </div>
    </div>
  );
}

export default SearchResult;
