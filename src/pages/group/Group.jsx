import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import AppContext from "../../app/features/context/AppContext";
import './ui/Group.css'
import ProductCard from "./ui/ProductCard";

export default function Group() {
    const {slug} = useParams();
    const {request, productGroup} = useContext(AppContext);
    const [pageData, setPageData] = useState( { products:[] } );
    useEffect(() => {
        request("/api/product-group/" + slug)
        .then(setPageData);
    }, [slug]);

    return <>
    <h1>Розділ {pageData.name}</h1>
    <h4>{pageData.description}</h4>
   <div className="border m-3 p-2 d-flex justify-content-center align-items-center">
  {productGroup.map(group => (
    <div key={group.id} className="border m-2 p-1 d-flex flex-column align-items-center">
      <Link className="nav-link badge text-bg-dark" to={"/group/" + group.slug}>
        {group.name}
      </Link>
      <img src={group.imageUrl} className="border mb-2 w-25" alt={group.name} />
      <span className="description text-center">{group.description}</span>
    </div>
  ))}
</div>
        
     <div className="row row-cols-1 row-cols-md-3 row-cols-lg-4 g-4 mt-4">
        {pageData.products.map(product => <ProductCard product={product} key={product.id} />
        )}
    </div>
    </>;
}