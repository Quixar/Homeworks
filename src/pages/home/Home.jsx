import { useContext, useEffect } from "react";
import { useState } from "react";
import AppContext from "../../app/features/context/AppContext";
import "./ui/Home.css"
import { Link } from "react-router-dom";

export default function Home() {
    const {request, productGroup} = useContext(AppContext);  // hook
    const [pageData, setPageData] = useState({productGroup:[]});

    useEffect(() => {
       request("/api/product-group")
       .then(setPageData)
    }, []);

   return <div>
        <div className="page-title">
            <img src={pageData.pageTitleImg} alt="pageTitleImg"/>
            <h1 className="display-4">{pageData.pageTitle}</h1>
        </div>
        <div className="row row-cols-1 row-cols-md-3 row-cols-lg-4 g-4 mt-4">
            {productGroup.map(grp => <GroupCard key={grp.slug} group={grp} />)}
        </div>
    </div>;
}



function GroupCard({group}) {
    return <div className="card h-100">
                    <Link to={"/group/" + group.slug} className="nav-link">
                        <img src={group.imageUrl} className="card-img-top" alt={group.name}/>
                    </Link>
                    <div className="card-body">
                        <h5 className="card-title">{group.name}</h5>
                        <p className="card-text">{group.description}</p>
                    </div>
                </div>
}