import React, { useEffect, useState } from "react";
import { Dropdown, FormControl } from "react-bootstrap";
import { Category } from "../../../services/api/Category";
import { Icategory } from "../../../services/api/Category/types";

interface IPropsDropdownSelector {
  onclick: (event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => void;
  categoryIDPutProp?: number;
}

const DropdownSelector = ({
  onclick,
  categoryIDPutProp,
}: IPropsDropdownSelector) => {
  const [data, setDate] = useState<Icategory[]>([]);

  useEffect(() => {
    Category.getAll().then((result) => {
      if (result instanceof Error) {
        alert(
          `Erro ao lista as categorias:\n message:\n  ${result.message}\n stack:\n  ${result.stack}`
        );
        return;
      } else {
        setDate(result);
        return;
      }
    });
  }, []);

  const categories: Icategory[] = [...data];
  type CustomToggleProps = {
    children?: React.ReactNode;
    onClick?: (event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => {};
  };

  // The forwardRef is important!!
  // Dropdown needs access to the DOM node in order to position the Menu
  const CustomToggle = React.forwardRef(
    (props: CustomToggleProps, ref: React.Ref<HTMLAnchorElement>) => (
      <a
        href=""
        ref={ref}
        onClick={(e) => {
          e.preventDefault();
          props.onClick && props.onClick(e);
        }}
      >
        {props.children}
        <span style={{ paddingLeft: "5px" }}>&#x25bc;</span>
      </a>
    )
  );

  type CustomMenuProps = {
    children?: React.ReactNode;
    style?: React.CSSProperties;
    className?: string;
    labeledBy?: string;
  };

  const CustomMenu = React.forwardRef(
    (props: CustomMenuProps, ref: React.Ref<HTMLDivElement>) => {
      const [value, setValue] = useState("");
      let reg = new RegExp(value, "i");

      return (
        <div
          ref={ref}
          style={props.style}
          className={props.className}
          aria-labelledby={props.labeledBy}
        >
          <FormControl
            autoFocus={true}
            className="mx-3 my-2 w-auto"
            placeholder="Digite para filtrar..."
            onChange={(e) => setValue(e.target.value)}
            value={value}
          />
          <ul
            className="list-unstyled overflow-auto"
            style={{ height: " 18.75rem" }}
          >
            {React.Children.toArray(props.children).filter(
              (child: any) => !value || reg.test(child.props.children)
            )}
          </ul>
        </div>
      );
    }
  );

  const [selectedCateg, setSelectedCateg] = useState(0);

  const theChosenCategory = () => {
    useEffect(() => {
      categoryIDPutProp && setSelectedCateg(categoryIDPutProp);
    }, []);

    const chosenCategory: Icategory | undefined = categories.find((categ) =>
      categoryIDPutProp ? categ.id === categoryIDPutProp : selectedCateg
    );

    const parent = categories.find(
      (category) => chosenCategory?.parentCategoryId === category.id
    );
    const grandparent = categories.find(
      (category) => parent?.parentCategoryId === category.id
    );

    return chosenCategory
      ? `${(grandparent && grandparent?.name + " >> ") || ""}` +
          `${
            (chosenCategory.parentCategoryId && parent?.name + " >> ") || ""
          }` +
          chosenCategory.name || ""
      : "Selecione uma Categoria";
  };

  const handleSelectCategory = (e: string | null) => {
    setSelectedCateg(Number(e));
  };

  return (
    <Dropdown id="category" onSelect={(e) => handleSelectCategory(e)}>
      <Dropdown.Toggle as={CustomToggle} id="dropdown-custom-components">
        {theChosenCategory()}
      </Dropdown.Toggle>

      <Dropdown.Menu as={CustomMenu}>
        {categories.map((category, index) => {
          return (
            <Dropdown.Item
              className="mb-3"
              id={(category.id && category.id.toString()) || ""}
              key={index}
              onClick={onclick}
              eventKey={category.id && category.id.toString()}
            >
              {category.name}
            </Dropdown.Item>
          );
        })}
      </Dropdown.Menu>
    </Dropdown>
  );
};

export { DropdownSelector };
