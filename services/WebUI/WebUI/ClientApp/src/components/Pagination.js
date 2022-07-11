import React from 'react';

const PaginationButton = ({i, page, numberOfPages, pageOnClick}) => {
  return (
    <>
      {page === i
        ?
        <li className="page-item disabled">
          <a className="page-link">{i}</a>
        </li>
        :
        <li className="page-item">
          <a className="page-link" onClick={() => pageOnClick(i)}>{i}</a>
        </li>
      }
    </>
  );
};

const Pagination = ({page, numberOfPages, pageOnClick}) => {
  return (
    <nav>
      <ul className="pagination justify-content-center">
        {page === 1
          ?
          <li className="page-item disabled">
            <a className="page-link">Previous</a>
          </li>
          :
          <li className="page-item">
            <a className="page-link" onClick={() => pageOnClick(page-1)}>Previous</a>
          </li>
        }

        {[...Array(numberOfPages)]
          .map((_, i) =>
            <PaginationButton
              key={i} i={i+1} page={page}
              numberOfPages={numberOfPages}
              pageOnClick={pageOnClick}
            />
          )}

        {page === numberOfPages
          ?
          <li className="page-item disabled">
            <a className="page-link">Next</a>
          </li>
          :
          <li className="page-item">
            <a className="page-link" onClick={() => pageOnClick(page-1)}>Next</a>
          </li>
        }
      </ul>
    </nav>
  )
}

export default Pagination;