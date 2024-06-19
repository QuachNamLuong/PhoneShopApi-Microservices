import styles from "./PhoneDetails.module.scss";
import classNames from "classnames/bind";
import { useEffect, useRef, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import toast from "react-hot-toast";

const cx = classNames.bind(styles);
function PhoneDetails() {
  const navigate = useNavigate();
  const thumbOptionRef = useRef();
  const thumbOptionContainerRef = useRef();
  const [mouseBehavior, setMouseBehavior] = useState(false);
  const [phone, setPhone] = useState([]);
  const [rom, setRom] = useState([]);
  const [color, setColor] = useState([]);
  const [currentPrice, setCurrentPrice] = useState(0);
  const [romActive, setRomActive] = useState(0);
  const [colorActive, setColorActive] = useState(0);
  const [currentThumb, setCurrentThumb] = useState();

  const handleScroll = (e) => {
    if (mouseBehavior) {
      const targetBig = thumbOptionContainerRef.current;
      const targetSmall = thumbOptionRef.current;
      const mousePosition = e.pageX - targetBig.offsetLeft;

      const move =
        (targetBig.offsetWidth * mousePosition) / targetSmall.offsetWidth;

      targetBig.style.transform = `translateX(${-move}px)`;
    }
  };

  const handleRom = (romId) => {
    const phoneId = window.location.pathname;
    setRomActive(romId);
    fetch("http://14.225.207.131:19001/api/Phone" + phoneId + "/" + romId)
      .then((res) => res.json())
      .then((res) => {
        setCurrentThumb(res[0].imageUrl);
        setColorActive(res[0].id);
        setColor(res);
        setCurrentPrice(res[0].price);
      })
      .catch((err) => console.error(err));
    setColorActive(0);
  };

  const handleColor = (colorId, colorPrice) => {
    setColorActive(colorId);
    setCurrentPrice(colorPrice);
  };

  useEffect(() => {
    const phoneId = window.location.pathname;
    fetch(
      "http://14.225.207.131:19001/api/Phone/GetPhoneBuiltInStorages" + phoneId
    )
      .then((res) => res.json())
      .then((res) => {
        console.log(res);
        setPhone(res.phone);
        setRom(res.storages);
        setRomActive(res.storages[0].id);
        fetch(
          "http://14.225.207.131:19001/api/Phone" +
            phoneId +
            "/" +
            res.storages[0].id
        )
          .then((res) => res.json())
          .then((res) => {
            setCurrentThumb(res[0].imageUrl);
            setColor(res);
            setCurrentPrice(res[0].price);
            setColorActive(res[0].id);

            console.log(res);
          })
          .catch((err) => console.error(err));
      })
      .catch((err) => console.error(err));
  }, []);

  const handleAddCart = () => {
    const user = JSON.parse(localStorage.getItem("user"));

    if (user) {
      fetch(
        `http://14.225.207.131:19001/api/PhoneOption/phone/${phone.id}/phoneColor/${colorActive}/builtInStorageId/${romActive}`
      )
        .then((res) => res.json())
        .then((res) => {
          fetch(
            `http://14.225.207.131:19003/api/Cart/user/${user.userId}/phoneOption/${res.id}`,
            {
              method: "POST",
              headers: {
                Accept: "application/json",
              },
            }
          )
            .then((res) => res.json())
            .then((res) => {
              if (res.length > 0) {
                toast.error("Sản phẩm đẫ tồn tại trong giỏ hàng");
              } else {
                toast.success("Đã thêm!");
              }
            })
            .catch((err) => console.error(err));
        })
        .catch((err) => console.error(err));
    } else {
      toast.error("Vui lòng đăng nhập để thêm vào giỏ hàng");
      navigate("/sign-in");
    }
  };

  const handlePay = () => {
    handleAddCart();
    navigate("/payment");
  };

  function insertDots(str) {
    // Reverse the string to start processing from the right
    const reversed = str.split("").reverse().join("");

    // Build the new string with dots every 3 characters
    let result = "";
    for (let i = 0; i < reversed.length; i += 3) {
      result += reversed.slice(i, i + 3);
      if (i < reversed.length - 3) {
        result += ".";
      }
    }
    result = "đ" + result;
    // Reverse the result to get the final order
    return result.split("").reverse().join("");
  }

  return (
    <div className={cx("wrapper")}>
      <h2 className={cx("brand-title")}></h2>
      <div className={cx("container")}>
        <div className={cx("details_img")}>
          <div className={cx("details_img-thumb")}>
            <img src={currentThumb} />
          </div>
          <div
            ref={thumbOptionRef}
            className={cx("details_img-thumb-option")}
            onMouseDown={(e) => {
              setMouseBehavior(true);
            }}
            onMouseUp={() => setMouseBehavior(false)}
            onMouseMove={(e) => handleScroll(e)}
            onMouseLeave={(e) => setMouseBehavior(false)}
          >
            <div
              ref={thumbOptionContainerRef}
              className={cx("thumb_option-container")}
            >
              {color.map((item, id) => {
                return (
                  <div key={id} className={cx("thumb_item")}>
                    <img
                      src={item.imageUrl}
                      alt={item.name}
                      onClick={() => setCurrentThumb(item.imageUrl)}
                    />
                  </div>
                );
              })}
            </div>
          </div>
        </div>
        <div className={cx("details_main")}>
          <div className={cx("details_main_container")}>
            <div className={cx("details_info")}>
              <div className={cx("info_wrapper")}>
                <h2 className={cx("info_name")}>{phone.name}</h2>
                <h4 className={cx("info_name")}>
                  {insertDots(currentPrice + "")}
                </h4>
                <div className={cx("info_option")}>
                  <h4 className={cx("ram_title")}>Bộ nhớ</h4>
                  <div className={cx("option_ram")}>
                    {rom.map((item, id) => {
                      if (item.capacity > 0) {
                        return (
                          <div
                            key={id}
                            className={cx("ram_item", {
                              active: item.id === romActive,
                            })}
                          >
                            <button onClick={() => handleRom(item.id)}>
                              <span>{item.capacity}</span>
                              <span></span>
                            </button>
                          </div>
                        );
                      }
                    })}
                  </div>

                  <h4 className={cx("color_title")}>Màu</h4>
                  <div className={cx("option_color")}>
                    {color.map((item, id) => {
                      if (item.price > 0) {
                        return (
                          <div
                            key={id}
                            className={cx("color_item", {
                              active: colorActive === item.id,
                            })}
                            onClick={() => {
                              setCurrentThumb(item.imageUrl);
                              handleColor(item.id, item.price);
                            }}
                          >
                            <img src={item.imageUrl} alt={item.name} />
                            <div>
                              <span className={cx("item_name")}>
                                {item.name}
                              </span>
                              <span>{insertDots(item.price + "")}</span>
                            </div>
                          </div>
                        );
                      }
                    })}
                  </div>
                </div>
              </div>
            </div>
            <div className={cx("details_action")}>
              <div className={cx("action_pay")}>
                <div onClick={handlePay}>
                  <button>Mua ngay</button>
                </div>
              </div>
              <div className={cx("action_addCart")}>
                <button onClick={handleAddCart}>+ Giỏ hàng</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default PhoneDetails;
