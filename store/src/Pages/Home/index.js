import PhoneDisplay from "~/components/PhoneDisplay";
import classNames from "classnames/bind";
import styles from "./Home.module.scss";
import { useEffect, useRef, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleLeft } from "@fortawesome/free-solid-svg-icons";
import { faAngleRight } from "@fortawesome/free-solid-svg-icons";

const cx = classNames.bind(styles);

function Home() {
  const [brand, setBrand] = useState([]);
  const brandRef = useRef();
  const [currentTranslate, setCurrentTranslate] = useState(0);

  useEffect(() => {
    fetch("http://14.225.207.131:19001/api/Brand/AllBrand")
      .then((res) => res.json())
      .then((res) => {
        console.log(res);
        brandRef.current.style.gridTemplateColumns = `repeat(${res.length}, 1fr)`;
        setBrand(res);
      })
      .catch((error) => console.error(error));
  }, []);

  const handleSlider = (direction) => {
    if (direction > 0) {
      setCurrentTranslate(
        -brandRef.current.children[0].children[0].offsetWidth +
          currentTranslate -
          16
      );
    } else {
      if (currentTranslate < 0) {
        setCurrentTranslate(
          brandRef.current.children[0].children[0].offsetWidth +
            currentTranslate +
            16
        );
      }
    }
  };

  useEffect(() => {
    brandRef.current.children[0].style.transform = `translateX(${currentTranslate}px)`;
  }, [currentTranslate]);

  return (
    <div className={cx("wrapper")}>
      <div className={cx("container")}>
        <div onClick={() => handleSlider(-1)} className={cx("brand-go-left")}>
          <FontAwesomeIcon icon={faAngleLeft} bounce />
        </div>
        <div ref={brandRef} className={cx("brand")}>
          <div className={cx("brand-buffer")}>
            {brand.map((brand, key) => {
              return (
                <div key={key} className={cx("brand-item")}>
                  <p className={cx("brand-title")}>{brand.name}</p>
                </div>
              );
            })}
          </div>
        </div>
        <div onClick={() => handleSlider(1)} className={cx("brand-go-right")}>
          <FontAwesomeIcon icon={faAngleRight} bounce />
        </div>
      </div>

      <PhoneDisplay />
    </div>
  );
}

export default Home;
