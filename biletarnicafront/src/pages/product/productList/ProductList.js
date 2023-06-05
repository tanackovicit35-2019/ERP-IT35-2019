import React, { useEffect, useState } from "react";
import styles from "./ProductList.module.scss";
import { BsFillGridFill } from "react-icons/bs";
import { FaListAlt } from "react-icons/fa";
import Search from "../../search/Search";
import ProductItem from "../productItem/ProductItem";
import Pagination from "../../pagination/Pagination";

const ProductList = ({ products }) => {
  const [grid, setGrid] = useState(true);
  const [search, setSearch] = useState("");
  const [sort, setSort] = useState("latest");

  const [currentPage, setCurrentPage] = useState(1);
  const [productsPerPage] = useState(10);
  const filteredProducts = products.filter((product) =>
  product.datumOdrzavanja.toLowerCase().includes(search.toLowerCase())
);

  const indexOfLastProduct = currentPage * productsPerPage;
  const indexOfFirstProduct = indexOfLastProduct - productsPerPage;
  //const currentProducts = products.slice(
  //  indexOfFirstProduct,
  //  indexOfLastProduct
  //);
  const currentProducts = filteredProducts
  .slice(indexOfFirstProduct, indexOfLastProduct)
  .sort((a, b) => {
    switch (sort) {
      case "latest":
        return b.id - a.id;
      case "lowest-price":
        return a.cenaKarte - b.cenaKarte;
      case "highest-price":
        return b.cenaKarte - a.cenaKarte;
      default:
        return 0;
    }
  });

  return (
    <div className={styles["product-list"]} id="product">
      <div className={styles.top}>
        <div className={styles.icons}>
          <BsFillGridFill
            size={22}
            color="purple"
            onClick={() => setGrid(true)}
          />

          <FaListAlt
            size={24}
            color="purple"
            onClick={() => setGrid(false)}
          />
        </div>
        {/* Search Icon */}
        <div>
          <Search size={17} value={search} onChange={(e) => setSearch(e.target.value)} />
        </div>
        {/* Sort Products */}
        <div className={styles.sort}>
          <label>Sort by:</label>
          <select value={sort} onChange={(e) => setSort(e.target.value)}>
            <option value="latest">Latest</option>
            <option value="lowest-price">Lowest Price</option>
            <option value="highest-price">Highest Price</option>
          </select>
        </div>
      </div>

      <div className={grid ? `${styles.grid}` : `${styles.list}`}>
        {products === 0 ? (
          <p>No product found.</p>
        ) : (
          <>
            {currentProducts.map((product) => {
              return (
                <div key={product.kartaID}>
                  <ProductItem {...product} grid={grid} ticket={product}/>
                </div>
              );
            })}
          </>
        )}
        <Pagination
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
          productsPerPage={productsPerPage}
           totalProducts={products.length}
        />
      </div>
    </div>
  );
};

export default ProductList;
