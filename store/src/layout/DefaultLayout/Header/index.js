import classNames from "classnames/bind";
import styles from "./Header.module.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import images from "~/assets/images";
import SearchResult from "~/components/SearchResult";
import { useEffect, useRef, useState } from "react";

import { Link } from "react-router-dom";

const cx = classNames.bind(styles);

function Header({ showSidebar, setShowSidebar }) {
  const [showResults, setShowResults] = useState(true);
  const [searchValue, setSearchValue] = useState("");
  const [searchResults, setSearchResults] = useState([]);

  const inputRef = useRef();

  const handleSearching = (e) => {
    setSearchValue(e.target.value);
  };

  useEffect(() => {
    if (searchValue.length > 0) {
      fetch(
        "http://14.225.207.131:8080/api/Phone/AllPhonesSellingFollowBrand?Name=" +
          searchValue
      )
        .then((res) => res.json())
        .then((res) => {
          setSearchResults(res);
        });
    }
  }, [searchValue]);

  return (
    <header className={cx("header")}>
      <div className={cx("wrapper")}>
        <div className={cx("wrapper-logo", { flex: true })}>
          <div className={cx("logo")}>
            <Link to="/">
              <img src={images.logo_gold} />
            </Link>
          </div>
        </div>
        <div className={cx("wrapper-searching", { flex: true })}>
          <input
            ref={inputRef}
            value={searchValue}
            type="text"
            className={cx("input")}
            onChange={(e) => handleSearching(e)}
            onFocus={() => setShowResults(true)}
          />
          <a>
            <FontAwesomeIcon icon={faMagnifyingGlass} />
          </a>
          <SearchResult
            results={searchResults}
            inputRef={inputRef}
            show={
              showResults && searchValue.length > 0 && searchResults.length > 0
            }
          />
        </div>
        <div className={cx("wrapper-user_sidebar", { flex: true })}>
          <div
            className={cx("user_sidebar")}
            onClick={() => setShowSidebar(!showSidebar)}
          >
            <FontAwesomeIcon icon={faBars} />
          </div>
        </div>
      </div>
    </header>
  );
}

export default Header;
